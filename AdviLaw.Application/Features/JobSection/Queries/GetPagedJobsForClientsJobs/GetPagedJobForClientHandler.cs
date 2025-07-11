using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Application.Features.JobSection.Queries.GetPagedJobs;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MailKit.Search;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GetPagedJobForClientHandler(
    IMapper _mapper,
    ResponseHandler _responseHandler,
    IUnitOfWork _unitOfWork

) : IRequestHandler<GetPagedJobForClientQuery, Response<PagedResponse<JobListForClientDTO>>>
{
    public async Task<Response<PagedResponse<JobListForClientDTO>>> Handle(GetPagedJobForClientQuery request, CancellationToken cancellationToken)
    {
        //var query = await _unitOfWork.Jobs.GetAllActivePublishedJobs();
        var query = await _unitOfWork.Jobs.GetAllAsync(
            //filter: j => j.Type == JobType.ClientPublishing && j.Status == JobStatus.NotAssigned && j.ClientId == request.ClientId,
            //filter: j => j.Type == JobType.ClientPublishing && j.ClientId == request.ClientId,
            filter: j => j.ClientId == request.ClientId,
            includes: new List<Expression<Func<Job, object>>>
            {
                j => j.Lawyer,
                j => j.Lawyer.User,
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

        var dtoList = _mapper.Map<List<JobListForClientDTO>>(pagedJobs);

        var pagedResponse = new PagedResponse<JobListForClientDTO>(
            dtoList, request.PageSize, totalCount, request.PageNumber
        );

        var response = _responseHandler.Success(pagedResponse);
        return response;
    }
}
