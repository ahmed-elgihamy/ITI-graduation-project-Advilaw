using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdviLaw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHandler _responseHandler;

        public UserController(IMediator mediator, ResponseHandler responseHandler)
        {
            _mediator = mediator;
            _responseHandler = responseHandler;
        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> Register([FromBody] CreateUserCommand command)
        {
            //re-validate
            var allowedRoles = new[] { "Client", "Lawyer", "Admin" };
            if (!allowedRoles.Contains(command.Role))
                return _responseHandler.BadRequest("Invalid role specified.");


            var userId = await _mediator.Send(command);
            return _responseHandler.Success(userId, new { timestamp = DateTime.UtcNow });
        }
    }
}