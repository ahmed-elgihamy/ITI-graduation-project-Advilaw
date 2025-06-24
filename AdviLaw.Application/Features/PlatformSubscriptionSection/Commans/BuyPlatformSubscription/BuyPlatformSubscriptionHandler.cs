using AdviLaw.Application.Basics;
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
    ) : IRequestHandler<BuyPlatformSubscriptionCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseHandler _responseHandler = responseHandler;
        private readonly UserManager<User> _userManager = userManager;
        public async Task<Response<bool>> Handle(BuyPlatformSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(u=>u.Lawyer).FirstOrDefaultAsync(u=>u.Id == request.LawyerId);
            if (user == null)
                return _responseHandler.BadRequest<bool>("Lawyer Not Found");
            var lawyer = user.Lawyer;
            //var lawyer = await _unitOfWork.Lawyers.FindFirstAsync(lawyer => lawyer.UserId == request.LawyerId);

            var subscriptionPlan = await _unitOfWork.PlatformSubscriptions.GetByIdAsync(request.SubscriptionTypeId);
            if (subscriptionPlan == null)
                return _responseHandler.BadRequest<bool>("Subscription Plan Not Found");

            var superAdminQuery = await _userManager.GetUsersInRoleAsync("Admin");
            var superAdmin = superAdminQuery.FirstOrDefault();
            if (superAdmin == null)
                return _responseHandler.BadRequest<bool>("No Admin Found");


            var payment = new Payment()
            {
                Type = PaymentType.SubscriptionPayment,
                SenderId = user.Id,
                ReceiverId = superAdmin.Id
            };

            //var payment = new Payment()
            //{
            //    Type = PaymentType.SubscriptionPayment,
            //    SenderId = request.LawyerId,
            //    ReceiverId = "7a21401d-74bd-448c-8047-61afd6175f45"
            //};

            var paymentAdded = await _unitOfWork.Payments.AddAsync(payment);

            var userSubscription = new UserSubscription()
            {
                LawyerId = lawyer.Id,
                SubscriptionTypeId = request.SubscriptionTypeId,
                Payment = payment,
            };

            await _unitOfWork.UserSubscriptions.AddAsync(userSubscription);
            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success(true);
        }
    }
}
