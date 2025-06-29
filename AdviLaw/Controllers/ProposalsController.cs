using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.ProposalSection.Command;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
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

    }
}
