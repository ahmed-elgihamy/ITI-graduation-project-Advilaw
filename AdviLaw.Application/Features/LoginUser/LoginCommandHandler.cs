using AdviLaw.Application.DTOs.Users;
using AdviLaw.Application.Features.LoginUser;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class LoginCommandHandler(
    UserManager<User> userManager,
    ITokenService tokenService,
    IUnitOfWork unitOfWork) : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return AuthResponse.Failure("Invalid email or password");

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.UserId = user.Id;

        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return AuthResponse.Success(accessToken, refreshToken.Token,user.Role.ToString());
    }
}
