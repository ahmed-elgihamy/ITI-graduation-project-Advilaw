using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdviLaw.Application.Features.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Response<object>>
    {
        private readonly UserManager<User> _userManager;
        private readonly ResponseHandler _responseHandler;

        public VerifyEmailCommandHandler( UserManager<User> userManager,ResponseHandler responseHandler)
        {
            _userManager = userManager;
            _responseHandler = responseHandler;
        }

        public async Task<Response<object>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            //1- Find the user by ID
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return _responseHandler.NotFound<object>("User not found.");
            }

            //2- Confirm the email
            var result = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<object>(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return _responseHandler.Success<object>(null, "Email verified successfully.");
        }
    }
} 