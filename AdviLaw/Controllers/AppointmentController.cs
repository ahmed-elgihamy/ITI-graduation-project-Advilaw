using AdviLaw.Application.Features.AppointmentSection.Commands.AcceptAppointment;
using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[Authorize]
        [HttpPost("{jobId}/create")]
        public async Task<IActionResult> CreateSchedule(int jobId, [FromBody] CreateAppointmentCommand command)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
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

        [Authorize(Roles = "Lawyer,Client")]
        [HttpPost("{appointmentId}/accept")]
        public async Task<IActionResult> AcceptAppointment(int appointmentId)
        {
            if (appointmentId <= 0)
            {
                return BadRequest("Invalid appointment ID.");
            }

            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
            {
                return Unauthorized("User ID not found in claims.");
            }
            int.TryParse(userIdStringified, out var userId);

            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (userRole == null)
            {
                return Unauthorized("User role not found in claims.");
            }

            var command = new AcceptAppointmentCommand()
            {
                AppointmentId = appointmentId,
                UserId = userId,
                UserRole = userRole switch
                {
                    "Lawyer" => UserRole.Lawyer,
                    "Client" => UserRole.Client,
                    _ => throw new UnauthorizedAccessException("Invalid user role.")
                }
            };

            var result = await _mediator.Send(command);
            return Ok(result);

        }
    }
}
