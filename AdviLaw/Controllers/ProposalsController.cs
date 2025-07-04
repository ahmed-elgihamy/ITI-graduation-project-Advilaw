using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.ProposalSection.Command;
using AdviLaw.Application.Features.ProposalSection.Command.AcceptProposal;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Application.Features.ProposalSection.Query.GetProposalsByJobIdForClient;
using AdviLaw.Application.Features.ProposalSection.Query.GetProposalsByJobIdForLawyer;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("{proposalId}")]
        public async Task<IActionResult> GetProposalsByIdAsync(int proposalId)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue("userId");

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            if (userRole == "Lawyer")
            {
                var requestDTO = new GetProposalsByJobIdForLawyerQuery(proposalId, int.Parse(userId));
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else if (userRole == "Client")
            {
                var requestDTO = new GetProposalsByJobIdForClientQuery(proposalId, int.Parse(userId));
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            return BadRequest("Invalid user role.");
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProposalAsync([FromBody] CreateProposalCommand createProposalCommand)
        {
            var stringifiedLawyerId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

            if (stringifiedLawyerId == null || !int.TryParse(stringifiedLawyerId, out var lawyerId))
            {
                return Unauthorized("User ID not found or invalid.");
            }

            createProposalCommand.LawyerId = lawyerId;

            var result = await _mediator.Send(createProposalCommand);
            return Ok(result);
        }

        [HttpPut("{proposalId}/accept")]
        public async Task<IActionResult> AcceptProposalAsync(int proposalId)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue("userId");
            if (userRole != "Client")
            {
                return Forbid("Only Clients can accept proposals.");
            }

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var clientId))
            {
                return Unauthorized("User ID not found or invalid.");
            }

            var command = new AcceptProposalCommand(proposalId, clientId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
