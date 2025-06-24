using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.CreatePlatformSubscription
{
    public class CreatePlatformSubscriptionHandler(
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler,
        IMapper mapper
    ) : IRequestHandler<CreatePlatformSubscriptionCommand, Response<PlatformSubscriptionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<PlatformSubscriptionDTO>> Handle(CreatePlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            //var points = _mapper.Map<List<SubscriptionPoint>>(request.Details);
            //await _unitOfWork.SubscriptionPoints.AddRangeAsync(points);
            //await _unitOfWork.SaveChangesAsync();
            var platformSubscription = _mapper.Map<PlatformSubscription>(request);
            var platformSubscriptionAdded = await _unitOfWork.PlatformSubscriptions.AddAsync(platformSubscription);
            await _unitOfWork.SaveChangesAsync();
            var platformSubscriptionDTO = _mapper.Map<PlatformSubscriptionDTO>(platformSubscriptionAdded);
            return _responseHandler.Success(platformSubscriptionDTO);
        }
    }
}
