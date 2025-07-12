using AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds;
using AdviLaw.Application.Features.Messages.DTOs;
using AdviLaw.Application.Features.Messages.Query;
using AdviLaw.Application.Features.SessionSection.Commands.HandleDisputedSession;
using AdviLaw.Application.Features.SessionSection.Commands.MarkSessionAsCompleted.AdviLaw.Application.Features.SessionSection.Commands;
using AdviLaw.Application.Features.SessionSection.Query;
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

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSessionDetails(int sessionId)
        {
            var result = await _mediator.Send(new GetSessionDetailsQuery(sessionId));
            return Ok(result); 
        }

        [HttpGet("{sessionId}/messages")]
        public async Task<ActionResult<List<ChatMessageDto>>> GetSessionMessages(int sessionId)
        {
            var messages = await _mediator.Send(new GetSessionMessagesQuery(sessionId));
            return Ok(messages);
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] MarkSessionAsCompletedCommand command)
        {
            if (id != command.SessionId)
                return BadRequest("ID mismatch.");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Session not found.");

            return NoContent(); // 204: Successfully updated
        }



    }

}
