using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionDetails
{
    public class GetPlatformSubscriptionDetailsQuery : IRequest<Response<PlatformSubscriptionDetailsDTO>>
    {
        public int Id { get; set; }
    }
}
