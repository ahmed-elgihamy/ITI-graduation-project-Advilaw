using AdviLaw.Application.Specializations;
using AdviLaw.Application.Specializations.Command.CreateSpecialization;
using AdviLaw.Application.Specializations.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsController(IMediator _mediator) : ControllerBase
    {
       // private readonly ISpecializationService _specializationService = specializationService;

        //[HttpGet]
        //public async Task<ActionResult> GetAll()
        //{
        //   / var specializations = await _specializationService.GetAllSpecialization();
        //    return Ok(specializations);
        //}


        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetById([FromRoute] int id)
        //{
        //    var Specialization = await _specializationService.GetById(id);

        //    if(Specialization is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Specialization);
        //}


        [HttpPost]
        public async Task<ActionResult> CreateSpecialization([FromBody] CreateSpeciallizationCommand command)
        {
            var id = await _mediator.Send(command);
            // CreatedAtAction(nameof(GetById), new { id }, null);
            return Ok();
        }

    }
}
