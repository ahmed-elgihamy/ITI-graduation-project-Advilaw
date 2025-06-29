using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.UserSubscriptionSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.LawyerSection.Queries.GetLawyerSubscriptions
{
    public class GetLawyerSubscriptionsQuery(string userId) : IRequest<Response<List<LawyerSubscriptionListDTO>>>
    {
        public string UserId { get; set; } = userId;
    }
}
