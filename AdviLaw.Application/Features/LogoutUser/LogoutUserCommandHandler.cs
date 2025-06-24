using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.LogoutUser
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var currentToken = await _unitOfWork.RefreshTokens
            .FindFirstAsync(rt => rt.Token == request.RefreshToken && rt.UserId == request.UserId);
            if (currentToken == null)
            {
                return false; //user is already logged out
            }
            currentToken.Revoked = DateTime.UtcNow;
            _unitOfWork.RefreshTokens.Update(currentToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
