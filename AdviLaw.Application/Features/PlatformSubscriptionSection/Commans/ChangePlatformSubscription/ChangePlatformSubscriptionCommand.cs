using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.ChangePlatformSubscription
{
    public class ChangePlatformSubscriptionCommand : IRequest<Response<PlatformSubscriptionDTO>>
    {
        public int Id { get; set; }
        public ChangePlatformSubscriptionCommand(int id)
        {
            Id = id;
        }
    }
}
