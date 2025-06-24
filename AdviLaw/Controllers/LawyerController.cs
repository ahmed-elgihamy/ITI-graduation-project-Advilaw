using AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LawyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] GetPagedLawyersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
