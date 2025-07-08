using System.Security.Claims;
using AdviLaw.Application.Features.Clients.Commands;
using AdviLaw.Application.Features.Clients.DTOs;
using AdviLaw.Application.Features.Clients.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("me/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdStringify = User.FindFirstValue("userId");
            var userId = int.TryParse(userIdStringify, out var id) ? id : default;
            var result = await _mediator.Send(new GetProfileQuery(userId));
            return Ok(result);
        }

        [HttpPatch("me/profile")]
        public async Task<IActionResult> EditProfile([FromBody] UpdateClientProfileCommand command)
        {
            var userIdStringify = User.FindFirstValue("userId");
            var userId = int.TryParse(userIdStringify, out var id) ? id : default;
            command.ClientId = userId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("me/profile/image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadProfileImage([FromForm] UplaodClientImageDTO dto)
        {
            var userIdStringify = User.FindFirstValue("userId");
            var userId = int.TryParse(userIdStringify, out var id) ? id : default;
            if (userId == 0) return Unauthorized();

            var command = new UpdateClientProfileImageCommand { ClientId = userId, Image = dto.Image };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
