
using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription
{
    public class DeletePlatformSubscriptionHandler(
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler
        ) : IRequestHandler<DeletePlatformSubscriptionQuery, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));

        public async Task<Response<object>> Handle(DeletePlatformSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var platform = await _unitOfWork.PlatformSubscriptions.GetByIdAsync(request.Id);
            if (platform == null)
            {
                return _responseHandler.NotFound<object>("No Subscription plan found to be deleted");
            }
            await _unitOfWork.PlatformSubscriptions.DeleteAsync(platform);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Deleted<object>();
        }
    }
}
