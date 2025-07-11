using AdviLaw.Application.Features.JobSection.Commands.AcceptConsultation;
using AdviLaw.Application.Features.JobSection.Commands.CreateJob;
using AdviLaw.Application.Features.JobSection.Commands.RejectConsultation;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.JobSection.Queries.GetClientActiveJobs;
using AdviLaw.Application.Features.JobSection.Queries.GetClientConsultations;
using AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForClient;
using AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForLawyer;
using AdviLaw.Application.Features.JobSection.Queries.GetLawyerActiveJobs;
using AdviLaw.Application.Features.JobSection.Queries.GetLawyerConsultations;
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
                var userId = User.FindFirst("userId")?.Value
                    ?? throw new UnauthorizedAccessException("User ID claim is missing");
                var requestDTO = _mapper.Map<GetPagedJobForClientQuery>(query);
                requestDTO.ClientId = int.TryParse(userId, out var clientId) ? clientId : default;
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
        }

        [HttpGet("me/ActiveJobs")]
        public async Task<IActionResult> GetMyActiveJobs([FromQuery] SearchQueryDTO query)
        {
            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
            {
                return Unauthorized("User ID not found in claims.");
            }
            int.TryParse(userIdStringified, out var userId);
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (role == "Lawyer")
            {
                var requestDTO = new GetLawyerActiveJobsQuery()
                {
                    LawyerId = userId,
                    Search = query.Search,
                    PageNumber = query.PageNumber,
                    PageSize = query.PageSize
                };
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else if (role == "Client")
            {
                var requestDTO = new GetClientActiveJobsQuery()
                {
                    ClientId = userId,
                    Search = query.Search,
                    PageNumber = query.PageNumber,
                    PageSize = query.PageSize
                };
                var result = await _mediator.Send(requestDTO);
                return Ok(result);
            }
            else
            {
                return BadRequest("Invalid user role. Only 'Lawyer' and 'Client' roles are allowed.");
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

            CreateJobCommand command = _mapper.Map<CreateJobCommand>(createJobDTO);
            command.UserId = userId;
            var result = await _mediator.Send(command);
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






        [HttpGet("me/consultations")]
        public async Task<IActionResult> GetMyConsultations([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
                return Unauthorized("User ID not found in claims.");
            int.TryParse(userIdStringified, out var userId);

            var result = await _mediator.Send(new GetLawyerConsultationsQuery
            {
                LawyerId = userId,
                PageNumber = page,
                PageSize = pageSize
            });

            return Ok(result);
        }

    
        [HttpPost("consultation/{id}/accept")]
        public async Task<IActionResult> AcceptConsultation(int id)
        {
            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
                return Unauthorized("User ID not found in claims.");
            int.TryParse(userIdStringified, out var userId);

            var result = await _mediator.Send(new AcceptConsultationCommand
            {
                JobId = id,
                LawyerId = userId
            });

            return Ok(result);
        }
        [HttpPost("consultation/{id}/reject")]
        public async Task<IActionResult> RejectConsultation(int id, [FromBody] RejectConsultationRequest request)
        {
            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
                return Unauthorized("User ID not found in claims.");
            int.TryParse(userIdStringified, out var userId);

            var result = await _mediator.Send(new RejectConsultationCommand
            {
                JobId = id,
                LawyerId = userId,
                Reason = request.Reason
            });

            return Ok(result);
        }

        [HttpGet("me/client-consultations")]
        public async Task<IActionResult> GetMyClientConsultations([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var userIdStringified = User.FindFirstValue("userId");
            if (userIdStringified == null)
                return Unauthorized("User ID not found in claims.");
            int.TryParse(userIdStringified, out var userId);

            var result = await _mediator.Send(new GetClientConsultationsQuery
            {
                ClientId = userId,
                PageNumber = page,
                PageSize = pageSize
            });

            return Ok(result);
        }

    }
}
