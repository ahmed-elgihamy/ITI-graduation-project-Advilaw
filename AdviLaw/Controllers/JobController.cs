using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForLawyer;
using AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForClient;
using AdviLaw.Application.Features.JobSection.Queries.GetPagedJobs;
using AdviLaw.Application.Features.Shared.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        //[Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] SearchQueryDTO query)
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "Lawyer")
            {
                var requestDTO = _mapper.Map<GetPagedJobForLawyerQuery>(query);
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException("User ID claim is missing");
                var requestDTO = _mapper.Map<GetPagedJobForClientQuery>(query);
                requestDTO.ClientId = int.TryParse(userId, out var clientId) ? clientId : default;
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (userRole == "Lawyer")
            {
                var requestDTO = new GetJobByIdLawyerQuery(id);
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else if (userRole == "Client")
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized("User ID not found in claims.");
                }
                var requestDTO = new GetJobByIdClientQuery(id);
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest("Invalid user role. Only 'Lawyer' and 'Client' roles are allowed.");
            }
        }

        //[Authorize(Roles = "Client")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateJobAsync([FromBody] CreateJobDTO createJobDTO)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in claims.");
            }


            //command.ClientId = int.TryParse(userId, out var clientId) ? clientId : (int?)null;
            CreateJobCommand command = _mapper.Map<CreateJobCommand>(createJobDTO);
            command.UserId = userId;
            var result = await _mediator.Send(command);
            //return CreatedAtAction(nameof(GetAllAsync), new { id = result.Id }, result);
            return Ok(result);
        }

        //[HttpPost]
        //[Authorize(Roles = "Client")]
        //[ProducesResponseType(typeof(Response<JobDto>), (int)HttpStatusCode.Created)]
        //[ProducesResponseType(typeof(Response<JobDto>), (int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        return Unauthorized(new Response<JobDto>("User ID not found in token"));
        //    }

        //    var command = new CreateJobCommand(jobDto, userId);
        //    var result = await _mediator.Send(command);

        //    return StatusCode((int)result.StatusCode, result);
        //}


    }
}
