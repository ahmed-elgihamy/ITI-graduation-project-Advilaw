using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.LawyerSection.DTOs;
using AdviLaw.Application.Features.LawyerSection.Queries.GetAllLawyers;
using AdviLaw.Application.Features.Shared.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetPagedLawyerHandler(
    ILogger<GetPagedLawyerHandler> logger,
    IMapper mapper,
    ResponseHandler responseHandler,
    IUnitOfWork unitOfWork
) : IRequestHandler<GetLawyerForAdminQuery, Response<PagedResponse<LawyerListDTO>>>
{
    public async Task<Response<PagedResponse<LawyerListDTO>>> Handle(GetLawyerForAdminQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching paged lawyers...");

        var query = unitOfWork.Lawyers.GetTableNoTracking();
        query = query
            .Include(l => l.User)
            .Include(l => l.Fields)
                .ThenInclude(f => f.JobField);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(l => l.User.UserName!.ToLower().Contains(request.Search.ToLower()));
        }

        if (request.IsApproved.HasValue)
        {
            query = query.Where(l => l.IsApproved == request.IsApproved.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var pagedData = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var dtoList = mapper.Map<List<LawyerListDTO>>(pagedData);

        var pagedResponse = new PagedResponse<LawyerListDTO>(
            dtoList, request.PageSize, totalCount, request.PageNumber
        );

        var response = responseHandler.Success(pagedResponse);
        return response;
    }
}
