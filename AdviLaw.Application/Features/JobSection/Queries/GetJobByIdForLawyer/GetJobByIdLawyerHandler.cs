using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace AdviLaw.Application.Features.JobSection.Queries.GetJobByIdForLawyer
{
    public class GetJobByIdLawyerHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IHttpContextAccessor httpContextAccessor
        )
        : IRequestHandler<GetJobByIdLawyerQuery, Response<JobDetailsForLawyerDTO>>
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<Response<JobDetailsForLawyerDTO>> Handle(GetJobByIdLawyerQuery request, CancellationToken cancellationToken)
        {
            if (request.JobId <= 0)
            {
                return _responseHandler.BadRequest<JobDetailsForLawyerDTO>("Invalid job ID.");
            }
            var userIdStringifed = _httpContextAccessor.HttpContext?.User?.FindFirstValue("userId");
            int.TryParse(userIdStringifed, out var userId);
            var job = await _unitOfWork.Jobs.GetJobByIdForLawyer(request.JobId);

            if (job == null)
            {
                return _responseHandler.NotFound<JobDetailsForLawyerDTO>("Job not found.");
            }

            if (job.LawyerId != userId && job.Status != JobStatus.NotAssigned)
            {
                return _responseHandler.BadRequest<JobDetailsForLawyerDTO>("You do not have permission to view this job.");
            }

            var dto = _mapper.Map<JobDetailsForLawyerDTO>(job);
            var response = _responseHandler.Success(dto, "Job details retrieved successfully.");
            return response;
        }
    }
}
