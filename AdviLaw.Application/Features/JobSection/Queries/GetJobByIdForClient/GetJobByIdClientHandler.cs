
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForClient
{
    public class GetJobByIdClientHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager

        )
        : IRequestHandler<GetJobByIdClientQuery, Response<JobDetailsForClientDTO>>
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

        public async Task<Response<JobDetailsForClientDTO>> Handle(GetJobByIdClientQuery request, CancellationToken cancellationToken)
        {
            if (request.JobId <= 0)
            {
                return _responseHandler.BadRequest<JobDetailsForClientDTO>("Invalid job ID.");
            }
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var job = await _unitOfWork.Jobs.GetJobByIdForClient(request.JobId);

            if (job == null)
            {
                return _responseHandler.NotFound<JobDetailsForClientDTO>("Job not found.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (job.ClientId != user?.Client.Id)
            {
                return _responseHandler.BadRequest<JobDetailsForClientDTO>("You do not have permission to view this job.");
            }

            var dto = _mapper.Map<JobDetailsForClientDTO>(job);
            var response = _responseHandler.Success(dto, "Job details retrieved successfully.");
            return response;
        }
    }
}
