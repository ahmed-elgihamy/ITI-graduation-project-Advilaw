using AdviLaw.Application.Features.Schedule.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;


        [HttpGet("api/lawyers/{id}/schedule")]
        public async Task<IActionResult> GetLawyerSchedule(int id)
        {
            var result = await _mediator.Send(new GetSchedulesByLawyerQuery(id));
            return Ok(result);
        }
    }
}
