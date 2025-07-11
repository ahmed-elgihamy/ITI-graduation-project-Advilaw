using AdviLaw.Application.Features.Reviews.Commands.CreateReview;
using AdviLaw.Application.Features.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("lawyers/{id}/reviews")]
    public async Task<IActionResult> GetReviewsByLawyer(Guid id)
    {
        var result = await _mediator.Send(new GetReviewsByLawyerQuery(id));
        return Ok(result);
    }

    [HttpPost("reviews")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        try
        {
            var reviewId = await _mediator.Send(command);
            return Ok(reviewId);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Something went wrong while saving your review." });
        }
    }
}
