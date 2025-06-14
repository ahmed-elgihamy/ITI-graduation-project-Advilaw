using AdviLaw.Application.Basics;
using AdviLaw.Application.Job.Command;
using AdviLaw.Application.Job.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(typeof(Response<JobDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Response<JobDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobDto jobDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new Response<JobDto>("User ID not found in token"));
            }

            var command = new CreateJobCommand(jobDto, userId);
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
