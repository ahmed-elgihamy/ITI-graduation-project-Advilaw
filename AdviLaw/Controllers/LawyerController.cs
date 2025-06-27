using AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile;
using AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers;
using AdviLaw.Application.Features.Reviews.Queries;
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

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetLawyerProfile(int id)
        {
            var result = await _mediator.Send(new GetLawyerProfileQuery(id));
            return Ok(result);
        }

        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviewsByLawyer(int id)
        {
            var result = await _mediator.Send(new GetReviewsByLawyerQuery(id));
            return Ok(result);
        }
    }
}
