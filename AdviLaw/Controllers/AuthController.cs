using AdviLaw.Application.Basics;
using AdviLaw.Application.DTOs.Users;

using AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer;
using AdviLaw.Application.Features.LoginUser;
using AdviLaw.Application.Features.LogoutUser;
using AdviLaw.Application.Features.RefreshToken;
using AdviLaw.Application.Features.RegisterUsers.Commands;
using AdviLaw.Application.Features.ResendReset;
using AdviLaw.Application.Features.ResetPassword;
using AdviLaw.Application.Features.SendResetCode;
using AdviLaw.Application.Features.VerifyEmail;
using AdviLaw.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHandler _responseHandler;



        public AuthController(IMediator mediator, ResponseHandler responseHandler)
        {
            _mediator = mediator;
            _responseHandler = responseHandler;

        }
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<object>> Register([FromForm] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.IsDefined(typeof(Roles), dto.Role))
                return _responseHandler.BadRequest<object>("Invalid role selected.");

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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : Unauthorized(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok(new { message = "Logged out successfully" }) : BadRequest(new { message = "Invalid refresh token or user" });
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string userId, [FromQuery] string token)
        {
            var command = new VerifyEmailCommand(userId, token);
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPost("send-reset-code")]
        public async Task<IActionResult> SendResetCode([FromBody] SendResetCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("resend-reset-code")]
        public async Task<IActionResult> ResendResetCode([FromBody] ResendResetCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result);
        }




    }
}
