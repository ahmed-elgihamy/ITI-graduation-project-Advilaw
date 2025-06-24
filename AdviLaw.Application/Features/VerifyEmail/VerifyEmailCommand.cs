using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.VerifyEmail
{
    //You send a confirmation email with a link that includes a token and the user ID.
    //When the user clicks the link, it hits your VerifyEmail endpoint, which calls _userManager.ConfirmEmailAsync().
    //If the email is confirmed, the user is redirected to the home page.
    //If the email is not confirmed, the user is redirected to the login page.


    public class VerifyEmailCommand : IRequest<Response<object>>
    {
        public string? UserId { get; set; }
        public string Token { get; set; }
        //parametrized constructor
        public VerifyEmailCommand(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }
    }
}