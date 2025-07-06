
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.ChangePlatformSubscription
{
    public class ChangePlatformSubscriptionHandler(IMapper mapper, IUnitOfWork unitOfWork, ResponseHandler responseHandler) : IRequestHandler<ChangePlatformSubscriptionCommand, Response<PlatformSubscriptionDTO>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;

        public async Task<Response<PlatformSubscriptionDTO>> Handle(ChangePlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.PlatformSubscriptions.GetByIdAsync(request.Id);
            if (subscription == null)
            {
                return _responseHandler.NotFound<PlatformSubscriptionDTO>("Subscription not found.");
            }
            subscription.IsActive = !subscription.IsActive;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var subscriptionDto = _mapper.Map<PlatformSubscriptionDTO>(subscription);
            var response = _responseHandler.Success(subscriptionDto);
            return response;
        }
    }
}
