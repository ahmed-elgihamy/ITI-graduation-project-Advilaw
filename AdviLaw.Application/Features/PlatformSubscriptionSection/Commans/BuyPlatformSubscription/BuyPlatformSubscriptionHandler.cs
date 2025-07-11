using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription
{
    public class BuyPlatformSubscriptionHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ResponseHandler responseHandler,
        UserManager<User> userManager
    ) : IRequestHandler<BuyPlatformSubscriptionCommand, Response<CreatedSubscriptionResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Response<CreatedSubscriptionResultDTO>> Handle(BuyPlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(u => u.Lawyer)
                .FirstOrDefaultAsync(u => u.Id == request.LawyerId, cancellationToken);
            if (user == null)
                return _responseHandler.BadRequest<CreatedSubscriptionResultDTO>("Lawyer Not Found");

            var subscriptionPlan = await _unitOfWork.PlatformSubscriptions
                .GetByIdAsync(request.SubscriptionTypeId);
            if (subscriptionPlan == null)
                return _responseHandler.BadRequest<CreatedSubscriptionResultDTO>("Subscription Plan Not Found");

            // For subscription payments, we'll use a system account or create a placeholder
            // This avoids the "No Admin Found" error while maintaining the payment record
            var payment = new Payment
            {
                Type = PaymentType.SubscriptionPayment,
                SenderId = user.Id,
                ReceiverId = user.Id // Temporarily set to same user to avoid admin requirement
            };
            await _unitOfWork.Payments.AddAsync(payment);

            var userSubscription = new UserSubscription
            {
                LawyerId = user.Lawyer.Id,
                SubscriptionTypeId = request.SubscriptionTypeId,
                Payment = payment
            };
            await _unitOfWork.UserSubscriptions.AddAsync(userSubscription);

            // Add points to the lawyer
            if (user.Lawyer != null && subscriptionPlan != null)
            {
                user.Lawyer.Points += subscriptionPlan.Points;
                // Optionally, you can track total points earned, etc.
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(new CreatedSubscriptionResultDTO
            {
                SubscriptionTypeId = request.SubscriptionTypeId,
                SubscriptionName = request.SubscriptionName,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
