using AdviLaw.Application.JobFields.Command.CreateJobField;
using AdviLaw.Application.JobFields.Command.DeleteJobField;
using AdviLaw.Application.JobFields.Command.UpdateJobField;
using AdviLaw.Application.JobFields.Query.GetAllJobFields;
using AdviLaw.Application.JobFields.Query.GetJobFieldById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobFieldsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobFieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobFieldCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateJobFieldCommand command)
        {
            var result = await _mediator.Send(new UpdateJobFieldRequest(id, command));
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteJobFieldCommand(id));

            if (!result.Succeeded)
            {
                
                return StatusCode((int)result.StatusCode, result);
            }
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllJobFieldsQuery());
            return Ok(result);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            
            var result = await _mediator.Send(new GetJobFieldByIdQuery(id));

          
            if (!result.Succeeded)
                return StatusCode((int)result.StatusCode, result);

         
            return Ok(result);
        }
    }
}
