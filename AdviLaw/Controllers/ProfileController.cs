using AdviLaw.Application.Features.LawyerProfile.Queries.GetLawyerProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdviLaw.Controllers
{
    [Route("api/lawyers/{id}/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetLawyerProfile(string id)
        {
            var result = await _mediator.Send(new GetLawyerProfileQuery(id)); 
            return Ok(result);
        }

    }
}
