using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Application.Features.Schedule.Commands.CreateSchedule;
using AdviLaw.Application.Features.Schedule.Queries;
using AdviLaw.Application.Features.Schedule.Queries.GetScheduleByLawyerIdNormal;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/lawyers/{id}/schedule")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetScheduleByLawyerId(Guid id)
    {
        var result = await _mediator.Send(new GetSchedulesByLawyerQuery(id));
        return Ok(result);
    }


    [HttpGet("normal")]
    public async Task<IActionResult> GetScheduleByLawyerIdNormal(Guid id)
    {
        var result = await _mediator.Send(new GetSchedulesByLawyerNormalQuery(id));
        return Ok(result);
    }


    [HttpPost]
    [Authorize] 
    public async Task<IActionResult> CreateSchedule(string id, [FromBody] CreateScheduleCommand command)
    {
        if (id == string.Empty || command == null)
        {
            return BadRequest("Invalid lawyer ID or command data.");
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if(id != userId)
        {
            return Forbid("You can only create a schedule for your own profile.");
        }
        command.UserId = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }


}
