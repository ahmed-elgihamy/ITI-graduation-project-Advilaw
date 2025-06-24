using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.CreatePlatformSubscription;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AutoMapper;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs.Profiling
{
    public class PlatformSubscriptionProfiling : Profile
    {
        public PlatformSubscriptionProfiling()
        {
            CreateMap<PlatformSubscription, PlatformSubscriptionDTO>().ReverseMap();
            CreateMap<PlatformSubscription, PlatformSubscriptionDetailsDTO>().ReverseMap();
            CreateMap<PlatformSubscription, CreatePlatformSubscriptionDTO>().ReverseMap();
            CreateMap<PlatformSubscription, CreatePlatformSubscriptionCommand>().ReverseMap();
        }
    }
}
