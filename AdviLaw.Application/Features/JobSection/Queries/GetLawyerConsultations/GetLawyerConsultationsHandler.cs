using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.JobSection.Queries.GetLawyerConsultations
{
    public class GetLawyerConsultationsHandler : IRequestHandler<GetLawyerConsultationsQuery, Response<PagedResponse<JobListDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        public GetLawyerConsultationsHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }

        public async Task<Response<PagedResponse<JobListDTO>>> Handle(GetLawyerConsultationsQuery request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.Jobs.GetAllAsync(
    filter: j => j.LawyerId == request.LawyerId && j.Type == JobType.LawyerProposal,
    includes: new List<Expression<Func<Job, object>>>
    {
           j => j.Client,
           j => j.Client.User,
           j => j.JobField
    }
);

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedJobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var dtoList = _mapper.Map<List<JobListDTO>>(pagedJobs);
            var pagedResponse = new PagedResponse<JobListDTO>(dtoList, request.PageSize, totalCount, request.PageNumber);

            return _responseHandler.Success(pagedResponse);
        }
    }
}