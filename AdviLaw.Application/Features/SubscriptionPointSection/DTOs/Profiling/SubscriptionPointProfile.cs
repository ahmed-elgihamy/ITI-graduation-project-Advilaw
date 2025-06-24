using AdviLaw.Domain.Entites.SubscriptionSection;
using AutoMapper;

namespace AdviLaw.Application.Features.SubscriptionPointSection.DTOs.Profiling
{
    public class SubscriptionPointProfile : Profile
    {
        public SubscriptionPointProfile() {
            CreateMap<SubscriptionPoint, SubscriptionPointDTO>().ReverseMap();
            CreateMap<SubscriptionPoint, CreateSubscriptionPointDTO>().ReverseMap();
        }
    }
}
