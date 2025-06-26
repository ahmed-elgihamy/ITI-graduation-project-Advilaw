using AdviLaw.Application.Features.Reviews.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("api/lawyers/{id}/reviews")]
        public async Task<IActionResult> GetReviewsByLawyer(int id)
        {
            var result = await _mediator.Send(new GetReviewsByLawyerQuery(id));
            return Ok(result);
        }

    }
}
