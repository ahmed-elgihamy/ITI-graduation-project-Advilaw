using AdviLaw.Application.DTOs.Users;

using AdviLaw.Domain.Entites.RefreshToken;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdviLaw.Application.Features.RefreshToken.Handlers
{
    public class RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        ITokenService tokenService) : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);
            if (storedToken == null || !storedToken.IsActive)
                return AuthResponse.Failure("Invalid or expired refresh token.");

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
                return AuthResponse.Failure("User not found.");

         
            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

     
            storedToken.Revoked = DateTime.UtcNow;

    
            newRefreshToken.UserId = user.Id;
            await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return AuthResponse.Success(newAccessToken, newRefreshToken.Token,user.Role.ToString());
        }
    }
}
