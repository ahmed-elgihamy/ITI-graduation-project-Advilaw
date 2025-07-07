using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Application.Features.Schedule.Queries;
using AdviLaw.Application.Features.ScheduleSection.Commands.CreateSchedule;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

   [HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Succeeded) return BadRequest(result.Message);
        return Ok(result.Data);
    }  
}
