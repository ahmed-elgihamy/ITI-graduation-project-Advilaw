using AdviLaw.Application.Features.PlatformSubscriptionSection.Commans.BuyPlatformSubscription;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AutoMapper;

namespace AdviLaw.Application.Features.UserSubscriptionSection.DTOs.Profiling
{
    public class UserSubscriptionProfile : Profile
    {
        public UserSubscriptionProfile()
        {
            CreateMap<UserSubscription, UserSubscriptionDetailsDTO>()
                .ForMember(dest => dest.LawyerId, opt => opt.MapFrom(src => src.Lawyer.User.Id))
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Lawyer.User.ImageUrl))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Lawyer.User.Country))
                .ReverseMap();

            CreateMap<UserSubscription, BuyPlatformSubscriptionCommand>().ReverseMap();

            CreateMap<UserSubscription, LawyerSubscriptionListDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SubscriptionType.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.SubscriptionType.Price))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.SubscriptionType.Points))
                .ReverseMap();

        }
    }
}
