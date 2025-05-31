using BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using server.Data.Database;
using server.Data.Entites.UserSection;
using server.Data.Tokens;
using server.Data.Tokens.RefreshTokenSection;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(AppDbContext db, IConfiguration configuration, IOptions<JwtSetting> _jwtOptions) : ControllerBase
    {
        private readonly JwtSetting jwtSetting = _jwtOptions.Value;
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
                return BadRequest("Invalid registration data.");

            if (string.IsNullOrEmpty(registerDto.Email) || string.IsNullOrEmpty(registerDto.Password))
                return BadRequest("Email and password are required.");

            if (string.IsNullOrEmpty(registerDto.FullName))
                return BadRequest("Full name is required.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            User user;

            if (registerDto.Role == "Lawyer")
                user = new Lawyer();
            else if (registerDto.Role == "Client")
                user = new Client();
            else if (registerDto.Role == "Admin")
            {
                if (!User.IsInRole("Admin"))
                    return Unauthorized();

                user = new Admin();
            }
            else
            {
                return BadRequest("Invalid role.");
            }

            user.Email = registerDto.Email;
            user.Name = registerDto.FullName;
            user.UserName = registerDto.UserName;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            var token = GenerateJwtToken(user);
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(jwtSetting.RefreshTokenValidityIn),
                UserId = user.Id,
                CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            await db.RefreshTokens.AddAsync(refreshToken);
            await db.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "User registered successfully.",
                Token = token,
                RefreshToken = refreshToken
            });
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            // Convert to int if needed (depends on your DB setup)
            var userId = int.Parse(userIdClaim.Value);

            var userTokens = db.RefreshTokens
                .Where(r => r.UserId == userId && !r.IsRevoked);

            foreach (var token in userTokens)
                token.IsRevoked = true;

            await db.SaveChangesAsync();

            return Ok("Logout successful. All refresh tokens revoked.");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (loginDto == null)
            {
                return BadRequest("Invalid login data.");
            }
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = await db.Users.FirstOrDefaultAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password.",
                });
            }
            var validPassword = await BCrypt.Net.BCrypt.Verify(LoginDto.Password, user.Password);
            if (!validPassword)
            {
                return Unauthorized(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid email or password.",
                });
            }
            var token = GenerateJwtToken(user);
            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(jwtSetting.RefreshTokenValidityIn),
                UserId = user.Id,
                CreatedByIp = HttpContext.Connection.RemoteIpAddress?.ToString()
            };

            await db.RefreshTokens.AddAsync(refreshToken);
            await db.SaveChangesAsync();
            var refreshTokenValidity = jwtSetting.RefreshTokenValidityIn;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidity);
            db.Update(user);
            await db.SaveChangesAsync();


            return Ok(new AuthResponseDto
            {
                Token = token,
                IsSuccess = true,
                Message = "Login successful.",
                RefreshToken = refreshToken,
            });
        }

        [HttpGet]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken(TokenDto tokenDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var principal = GetPrincipalFromExpiredToken(tokenDto.Token);
            if (principal is null)
                return BadRequest("Invalid token.");

            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == tokenDto.Email);
            if (user is null)
                return BadRequest("User not found.");

            var existingToken = await db.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(rt =>
                    rt.Token == tokenDto.RefreshToken &&
                    rt.User.Email == tokenDto.Email &&
                    !rt.IsRevoked &&
                    rt.ExpiresAt > DateTime.UtcNow);

            if (existingToken is null)
                return BadRequest("Invalid or expired refresh token.");

            // Revoke old token
            existingToken.IsRevoked = true;

            // Create new refresh token
            var newRefreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(jwtSetting.RefreshTokenValidityIn),
                UserId = user.Id
            };

            await db.RefreshTokens.AddAsync(newRefreshToken);
            await db.SaveChangesAsync();

            var newJwtToken = GenerateJwtToken(user);

            return Ok(new AuthResponseDto
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token,
                IsSuccess = true,
                Message = "Token refreshed."
            });
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey)),
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principal;

        }

        [Authorize]
        [HttpGet("details")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.FindAsync(currentUserId);

            if (user == null)
            {
                return NotFound(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User not found.",
                });
            }

            var userDetails = new UserDetailsDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user switch
                {
                    Admin => "Admin",
                    Lawyer => "Lawyer",
                    Client => "Client",
                    _ => "Unknown"
                },
            };
            return Ok(userDetails);
        }
        // authorize on admin role only
        [Authorize(Roles = "Admin")]
        [HttpGet("getallusers")]
        public async Task<ActionResult<List<UserDetailsDto>>> GetAllUsers()
        {
            var users = await db.Users.ToListAsync();

            var result = users.Select(u => new UserDetailsDto
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                Roles = u switch
                {
                    Admin => "Admin",
                    Lawyer => "Lawyer",
                    Client => "Client",
                    _ => "Unknown"
                }
            }).ToList();


            return Ok(result);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSetting.SecurityKey);
            string role;

            if (user is Admin) role = "Admin";
            else if (user is Lawyer) role = "Lawyer";
            else if (user is Client) role = "Client";
            else role = "Unknown";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, role),
                new(JwtRegisteredClaimNames.Aud, jwtSetting.ValidAudience),
                new(JwtRegisteredClaimNames.Iss, jwtSetting.ValidIssuer),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[12];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
