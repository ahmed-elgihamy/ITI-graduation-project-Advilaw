using System.Threading.Tasks;
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Users.Commands.CreateUser;
using AdviLaw.Domain.Enums;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            if (!Enum.IsDefined(typeof(Roles), command.Role))
                return _responseHandler.BadRequest("Invalid role.");




            var userId = await _mediator.Send(command);
            return _responseHandler.Success(userId, new { timestamp = DateTime.UtcNow });
        }
    }
}