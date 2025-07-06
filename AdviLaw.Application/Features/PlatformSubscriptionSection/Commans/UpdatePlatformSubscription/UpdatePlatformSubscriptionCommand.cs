using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.UpdatePlatformSubscription
{
    public class UpdatePlatformSubscriptionCommand : IRequest<Response<PlatformSubscriptionDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }

    }
}
