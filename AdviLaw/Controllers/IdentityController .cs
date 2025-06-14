using AdviLaw.Domain.Entites.Auth;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdviLaw.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly AdviLawDBContext _context;
        private readonly IConfiguration _configuration;
        public IdentityController(UserManager<User> userManager, AdviLawDBContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // 1. إنشاء الـ User
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                City = request.City,
                Country = request.Country,
                CountryCode = request.CountryCode,
                PostalCode = request.PostalCode,
                NationalityId = request.NationalityId,
                ImageUrl = request.ImageUrl,
                Gender = request.Gender,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            try
            {
                // 2. تحديد نوع المستخدم
                switch (request.UserType.ToLower())
                {
                    case "lawyer":
                        var lawyer = new Lawyer
                        {
                            UserId = user.Id,
                            ProfileHeader = request.ProfileHeader ?? string.Empty,
                            ProfileAbout = request.ProfileAbout ?? string.Empty,
                            LawyerCardID = request.LawyerCardID ?? 0,
                            barCardImagePath = request.BarCardImagePath ?? string.Empty,
                            barAssociationCardNumber = request.BarAssociationCardNumber ?? 0,
                            Bio = request.Bio ?? string.Empty,
                            IsApproved = false
                        };
                        _context.Lawyers.Add(lawyer);
                        break;

                    case "client":
                        var client = new Client
                        {
                            UserId = user.Id
                        };
                        _context.Clients.Add(client);
                        break;

                    case "admin":
                        var admin = new Admin
                        {
                            UserId = user.Id
                        };
                        _context.Admins.Add(admin);
                        break;

                    default:
                        return BadRequest("User type is invalid. Must be 'Lawyer', 'Client' or 'Admin'.");
                }

                await _context.SaveChangesAsync();
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "An error occurred while saving data.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }




        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return Unauthorized("Invalid email or password.");

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            refreshToken.UserId = user.Id;
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken.Token
            });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var storedToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (storedToken == null || !storedToken.IsActive)
                return Unauthorized("Invalid or expired refresh token.");

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null) return Unauthorized("User not found.");

          
            storedToken.Revoked = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            newRefreshToken.UserId = user.Id;

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            var newAccessToken = GenerateJwtToken(user);

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken.Token
            });
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
