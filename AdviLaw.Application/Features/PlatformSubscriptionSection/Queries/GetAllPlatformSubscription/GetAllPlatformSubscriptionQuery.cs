using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.Queries.GetPlatformSubscriptionPlan
{
    public class GetAllPlatformSubscriptionQuery : IRequest<Response<List<PlatformSubscriptionDTO>>>
    {
    }
}
