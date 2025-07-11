
using AdviLaw.Application.Basics;
using AdviLaw.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription
{
    public class DeletePlatformSubscriptionHandler(
        IUnitOfWork unitOfWork,
        ResponseHandler responseHandler
        ) : IRequestHandler<DeletePlatformSubscriptionCommand, Response<object>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly ResponseHandler _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));

        public async Task<Response<object>> Handle(DeletePlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            // Get platform subscription with details
            var platform = await _unitOfWork.PlatformSubscriptions.GetByIdDetails(request.Id);
            if (platform == null)
            {
                return _responseHandler.NotFound<object>("No Subscription plan found to be deleted");
            }

            // Check for existing user subscriptions
            var existingUserSubscriptions = await _unitOfWork.UserSubscriptions.FindFirstAsync(
                us => us.SubscriptionTypeId == request.Id
            );
            if (existingUserSubscriptions != null)
            {
                return _responseHandler.BadRequest<object>("Cannot delete this plan because it has active user subscriptions. Please deactivate the plan instead or wait for all user subscriptions to expire.");
            }

            // Check for subscription points (details) and delete them first
            if (platform.Details != null && platform.Details.Any())
            {
                foreach (var detail in platform.Details)
                {
                    await _unitOfWork.SubscriptionPoints.DeleteAsync(detail);
                }
            }

            await _unitOfWork.PlatformSubscriptions.DeleteAsync(platform);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Deleted<object>();
        }
    }
}
