using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.UpdatePlatformSubscription
{
    public class UpdatePlatformSubscriptionHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IMapper mapper) : IRequestHandler<UpdatePlatformSubscriptionCommand, Response<PlatformSubscriptionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<PlatformSubscriptionDTO>> Handle(UpdatePlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _unitOfWork.PlatformSubscriptions.GetByIdAsync(request.Id);
            if (subscription == null)
            {
                return _responseHandler.NotFound<PlatformSubscriptionDTO>("Subscription not found.");
            }
            subscription.Name = request.Name;
            subscription.Price = request.Price;
            subscription.Points = request.Points;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var subscriptionDto = _mapper.Map<PlatformSubscriptionDTO>(subscription);
            var response = _responseHandler.Success(subscriptionDto);
            return response;
        }
    }
}
