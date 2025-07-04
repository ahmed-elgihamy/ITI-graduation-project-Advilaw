using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost("{jobId}/create")]
        public async Task<IActionResult> CreateSchedule(int jobId, [FromBody] CreateAppointmentCommand command)
        {
            var role = User.FindFirst("Role")?.Value;
            if (role == null)
            {
                return Unauthorized("User role not found in claims.");
            }
            if (jobId <= 0 || command == null)
            {
                return BadRequest("Invalid job ID or command data.");
            }
            command.UserRole = role == "Lawyer" ? UserRole.Lawyer : UserRole.Client;

            command.JobId = jobId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
