using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using AdviLaw.Application.Features.SubscriptionPointSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.CreatePlatformSubscription
{
    public class CreatePlatformSubscriptionCommand : IRequest<Response<PlatformSubscriptionDTO>>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }
        //public int Duration { get; set; } = 30; //30 days Default
        public List<CreateSubscriptionPointDTO>? Details { get; set; } = new();
    }
}
