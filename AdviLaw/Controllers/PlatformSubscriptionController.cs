using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.CreatePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionDetails;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionPlan;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformSubscriptionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "Lawyer")]
        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans()
        {
            var result = await _mediator.Send(new GetPlatformSubscriptionPlanQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPlatformSubscriptionQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPlatformSubscriptionDetailsQuery() { Id = id });
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePlatformSubscriptionQuery() { Id = id });
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<IActionResult> Create(CreatePlatformSubscriptionQuery createPlatformSubscriptionQuery)
        {
            var result = await _mediator.Send(createPlatformSubscriptionQuery);
            return Ok(result);
        }
    }
}
