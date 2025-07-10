using AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds;
using AdviLaw.Application.Features.SessionSection.Commands.HandleDisputedSession;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionRefundsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SessionRefundsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [HttpPost("release-funds")]
        // public async Task<IActionResult> ReleaseFunds([FromBody] ReleaseSessionFundsCommand cmd)
        // {
        //     var result = await _mediator.Send(cmd);
        //     if (!result.Succeeded) return BadRequest(result.Message);
        //     return Ok("Funds released to lawyer.");
        // }

        [HttpPost("handle-dispute")]
        public async Task<IActionResult> HandleDispute([FromBody] HandleDisputedSessionCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.Succeeded) return BadRequest(result.Message);
            return Ok("Dispute resolved and payment handled.");
        }
    }

}
