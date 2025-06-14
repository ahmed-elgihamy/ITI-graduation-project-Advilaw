using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Users;
using AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer;
using AdviLaw.Application.Features.LoginUser;
using AdviLaw.Application.Features.RegisterUsers;
using AdviLaw.Application.Features.RegisterUsers.Commands;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHandler _responseHandler ;



        public AuthController(IMediator mediator, ResponseHandler responseHandler)
        {
            _mediator = mediator;
            _responseHandler = responseHandler;

        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //validate the role
            if (!Enum.IsDefined(typeof(Roles), dto.Role))
                return _responseHandler.BadRequest<object>("Invalid role Selected.");

            var command = new RegisterUserCommand(dto);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                return _responseHandler.BadRequest<object>(result.Message);
            }
            return _responseHandler.Success(result.Data, new { timestamp = DateTime.UtcNow });

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : Unauthorized(result);
        }

        //[HttpPost("refresh-token")]
        //public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return result.Succeeded ? Ok(result) : Unauthorized(result);
        //}


    }
}
