using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdviLaw.Application.Features.AdminSection.Commands;
using AdviLaw.Application.Basics;
using System.Security.Claims;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Application.Features.AdminSection.Queries;
using AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers;
using AdviLaw.Application.Features.Clients.Queries;

namespace AdviLaw.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ResponseHandler _responseHandler;
        public AdminController(IMediator mediator, ResponseHandler responseHandler)
        {
            _mediator = mediator;
            _responseHandler = responseHandler;
        }

        [HttpPost("clients/{id}/approve")]
        public async Task<IActionResult> ApproveClient(int id, CancellationToken cancellationToken)
        {
            var command =  new ApproveClientCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Message);
        }

        [HttpPost("lawyers/{id}/approve")]
        public async Task<IActionResult> ApproveLawyer(int id, CancellationToken cancellationToken)
        {
            var command = new ApproveLawyerCommand(id);
            var response = await _mediator.Send(command, cancellationToken);

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Message);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> EditProfile([FromBody] EditAdminProfileDto dto)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var command = new EditAdminProfileCommand(dto, userId);
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("admins")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var result = await _mediator.Send(new GetAllAdminsQuery());
            return Ok(result);
        }

        [HttpPut("admins/{userId}/role")]
        public async Task<IActionResult> AssignAdminRole(string userId, [FromBody] AssignAdminRoleDto dto)
        {
            var command = new AssignAdminRoleCommand(userId, dto.Role);
            var result = await _mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

  

        [HttpGet("lawyers/pending")]
        public async Task<IActionResult> GetPendingLawyers()
        {
            var result = await _mediator.Send(new GetPagedLawyersQuery { PageNumber = 1, PageSize = 100, IsApproved = false });
            return Ok(result.Data.Data);
        }

        [HttpGet("clients/pending")]
        public async Task<IActionResult> GetPendingClients()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());
            return Ok(result);
        }
    }
}
