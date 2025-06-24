using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionDetails;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionPlan
{
    public class GetPlatformSubscriptionDetailsHandler(IMapper mapper,
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler
    ) : IRequestHandler<GetPlatformSubscriptionDetailsQuery, Response<PlatformSubscriptionDetailsDTO>>
    {
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));

        public async Task<Response<PlatformSubscriptionDetailsDTO>> Handle(GetPlatformSubscriptionDetailsQuery request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.PlatformSubscriptions.GetByIdDetails(request.Id);
            if (subscription == null)
            {
                return _responseHandler.NotFound<PlatformSubscriptionDetailsDTO>("Subscription plan not found");
            }

            var dto = _mapper.Map<PlatformSubscriptionDetailsDTO>(subscription);
            var response = _responseHandler.Success(dto, "Job details retrieved successfully.");
            return response;
        }
    }
}

