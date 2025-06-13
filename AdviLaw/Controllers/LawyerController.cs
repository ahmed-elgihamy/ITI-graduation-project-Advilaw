using AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdviLaw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LawyerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LawyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateLawyerCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lawyerId = await _mediator.Send(command);
            return Ok(lawyerId);
        }
    }
} 