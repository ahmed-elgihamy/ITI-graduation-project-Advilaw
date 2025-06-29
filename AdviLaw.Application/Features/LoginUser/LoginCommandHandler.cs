using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Users;
using AdviLaw.Application.Features.LoginUser;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class LoginCommandHandler(
    UserManager<User> userManager,
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    ILogger<LoginCommandHandler> logger
) : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<LoginCommandHandler> _logger = logger;

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = await _userManager.Users
            .Include(u => u.Lawyer)
            .Include(u => u.Client)
            .Include(u => u.Admin)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            _logger.LogWarning("Invalid login attempt for email: {Email}", request.Email);
            return AuthResponse.Failure("Invalid email or password");
        }

        //if (!user.EmailConfirmed)
        //{
        //    _logger.LogWarning("Login attempt with unconfirmed email: {Email}", request.Email);
        //    return AuthResponse.Failure("Please confirm your email before logging in.");
        //}

        //  delete old refresh tokens (single-device login behavior)
        await _unitOfWork.RefreshTokens.DeleteAllAsync(rt => rt.UserId == user.Id);

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.UserId = user.Id;

        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User {Email} logged in successfully.", user.Email);

        return AuthResponse.Success(accessToken, refreshToken.Token, user.Role.ToString());
    }
}
