using MediatR;

namespace AdviLaw.Application.Features.LogoutUser
{
    public class LogoutUserCommand :IRequest<bool>
    {
        public string? UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
