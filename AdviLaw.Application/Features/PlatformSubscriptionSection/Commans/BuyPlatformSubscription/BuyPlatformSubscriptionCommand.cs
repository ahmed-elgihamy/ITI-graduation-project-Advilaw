using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription
{
    public class BuyPlatformSubscriptionCommand : IRequest<Response<CreatedSubscriptionResultDTO>>
    {
        public string LawyerId { get; set; } = string.Empty;
        public int SubscriptionTypeId { get; set; }

        public string SubscriptionName { get; set; } = string.Empty;
        //public int PaymentId { get; set; }
    }
}
