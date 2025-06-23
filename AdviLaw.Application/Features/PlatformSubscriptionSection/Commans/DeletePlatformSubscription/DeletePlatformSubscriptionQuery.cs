using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.DeletePlatformSubscription
{
    public class DeletePlatformSubscriptionQuery : IRequest<Response<object>>
    {
        public int Id { get; set; }
    }
}
