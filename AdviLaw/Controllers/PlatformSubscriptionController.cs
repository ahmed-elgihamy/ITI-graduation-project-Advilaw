using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.ChangePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.CreatePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.UpdatePlatformSubscription;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionDetails;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionPlan;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        //[Authorize(Roles = "Admin")]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPlatformSubscriptionQuery());
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPlatformSubscriptionDetailsQuery() { Id = id });
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePlatformSubscriptionCommand() { Id = id });
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("")]
        public async Task<IActionResult> Create(CreatePlatformSubscriptionCommand createPlatformSubscriptionQuery)
        {
            var result = await _mediator.Send(createPlatformSubscriptionQuery);
            return Ok(result);
        }

        [Authorize(Roles = "Lawyer")]
        [HttpPost("{id}/buy")]
        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new BuyPlatformSubscriptionCommand()
            {
                LawyerId = userId!,
                SubscriptionTypeId = id,
            });
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("{id}/change")]
        public async Task<IActionResult> Change([FromRoute] int id)
        {
            var changePlatformSubscriptionCommand = new ChangePlatformSubscriptionCommand(id);
            var result = await _mediator.Send(changePlatformSubscriptionCommand);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePlatformSubscriptionCommand command)
        {
            if (id <= 0)
            {
                return BadRequest("Subscription ID mismatch.");
            }
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
