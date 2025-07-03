using AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile;
using AdviLaw.Application.Features.Lawyers.Queries.GetAllLawyers;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers;
using AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerPayments;
using AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerReviews;
using AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerSubscriptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("all")]
        public async Task<ActionResult<LawyerListDTO>> GetAll([FromQuery]GetAllLawyersQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
   
        [HttpGet("api/lawyers/{id}/profile")]
        public async Task<IActionResult> GetLawyerProfile(int id)
        {
            var result = await _mediator.Send(new GetLawyerProfileQuery(id));
            return Ok(result);
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("me/reviews")]
        public async Task<IActionResult> GetLawyerReviews(string? Search, int? PageNumber, int? PageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetLawyerReviewsQuery(userId, Search, PageNumber, PageSize));
            return Ok(result);
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("me/payments")]
        public async Task<IActionResult> GetLawyerPayments(string? Search, int? PageNumber, int? PageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetLawyerPaymentsQuery(userId, Search, PageNumber, PageSize));
            return Ok(result);
        }

        [Authorize(Roles = "Lawyer")]
        [HttpGet("me/subscriptions")]
        public async Task<IActionResult> GetLawyerSubscriptions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetLawyerSubscriptionsQuery(userId));
            return Ok(result);
        }
    }
}
