using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription
{
    public class DeletePlatformSubscriptionCommand : IRequest<Response<object>>
    {
        public int Id { get; set; }
    }
}
