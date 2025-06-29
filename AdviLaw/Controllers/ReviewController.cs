using AdviLaw.Application.Features.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/lawyers/{id}/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet]
        public async Task<IActionResult> GetReviewsByLawyer(int id)
        {
            var result = await _mediator.Send(new GetReviewsByLawyerQuery(id));
            return Ok(result);
        }


    }
}
