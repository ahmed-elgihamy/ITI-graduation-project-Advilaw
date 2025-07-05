using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.JobSection.Queries.GetClientActiveJobs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Application.Features.JobSection.Queries.GetLawyerActiveJobs
{
    public class GetLawyerActiveJobsHandler(
        IMapper _mapper,
        ResponseHandler _responseHandler,
        IUnitOfWork _unitOfWork

    ) : IRequestHandler<GetLawyerActiveJobsQuery, Response<PagedResponse<JobListForLawyerDTO>>>
    {
        public async Task<Response<PagedResponse<JobListForLawyerDTO>>> Handle(GetLawyerActiveJobsQuery request, CancellationToken cancellationToken)
        {
            var query = await _unitOfWork.Jobs.GetAllAsync(
                filter: j =>
                    j.Status != JobStatus.NotAssigned &&
                    j.LawyerId == request.LawyerId,
                includes: new List<Expression<Func<Job, object>>>
                {
                    j => j.Client,
                    j => j.Client.User,
                    j => j.JobField
                }
            );

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(j => EF.Functions.Like(j.Header, $"%{request.Search}%")).OrderByDescending(j => j.Id);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var pagedJobs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var dtoList = _mapper.Map<List<JobListForLawyerDTO>>(pagedJobs);

            var pagedResponse = new PagedResponse<JobListForLawyerDTO>(
                dtoList, request.PageSize, totalCount, request.PageNumber
            );

            var response = _responseHandler.Success(pagedResponse);
            return response;
        }
    }
}
