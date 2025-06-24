using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription
{
    public class BuyPlatformSubscriptionCommand : IRequest<Response<bool>>
    {
        public string LawyerId { get; set; } = string.Empty;
        public int SubscriptionTypeId { get; set; }
        //public int PaymentId { get; set; }
    }
}
