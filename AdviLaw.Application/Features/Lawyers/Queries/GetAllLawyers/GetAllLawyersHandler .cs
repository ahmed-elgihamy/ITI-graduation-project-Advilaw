using AdviLaw.Application.Common;
using AdviLaw.Application.Features.Lawyers.Queries.DTOs;
using AdviLaw.Application.Features.Lawyers.Queries.GetAllLawyers;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetAllLawyersHandler : IRequestHandler<GetAllLawyersQuery, PagedResult<LawyerListItemDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllLawyersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<LawyerListItemDto>> Handle(GetAllLawyersQuery request, CancellationToken cancellationToken)
    {
        var (lawyers, totalCount) = await _unitOfWork.Lawyers.GetAllMatchingAsync(
       request.SearchPhrase,
       request.PageSize,
       request.PageNumber
      );

        var resDto = _mapper.Map<List<LawyerListItemDto>>(lawyers);

        var result = new PagedResult<LawyerListItemDto>(resDto, totalCount, request.PageSize, request.PageNumber);
        return result;

    }

}
