using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.Reviews.DTOs;
using AdviLaw.Application.Features.Reviews.Queries;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

public class GetReviewsByLawyerHandler : IRequestHandler<GetReviewsByLawyerQuery, Response<List<ReviewDTO>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ResponseHandler _responseHandler;

    public GetReviewsByLawyerHandler(IUnitOfWork unitOfWork, IMapper mapper, ResponseHandler responseHandler)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _responseHandler = responseHandler;
    }

    public async Task<Response<List<ReviewDTO>>> Handle(GetReviewsByLawyerQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.Reviews.GetReviewsByLawyerId(request.LawyerId);
        var reviewDtos = _mapper.Map<List<ReviewDTO>>(reviews);
        return _responseHandler.Success(reviewDtos);
    }
}

