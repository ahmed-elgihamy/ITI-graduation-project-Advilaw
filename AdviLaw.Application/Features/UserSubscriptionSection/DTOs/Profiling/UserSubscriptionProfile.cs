using AdviLaw.Domain.Entites.SubscriptionSection;
using AutoMapper;

namespace AdviLaw.Application.Features.UserSubscriptionSection.DTOs.Profiling
{
    public class UserSubscriptionProfile : Profile
    {
        public UserSubscriptionProfile()
        {
            CreateMap<UserSubscription, UserSubscriptionDTO>()
                .ForMember(dest => dest.LawyerId, opt => opt.MapFrom(src => src.Lawyer.User.Id))
                .ForMember(dest => dest.LawyerName, opt => opt.MapFrom(src => src.Lawyer.User.UserName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Lawyer.User.ImageUrl))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Lawyer.User.Country))
                .ReverseMap();
        }
    }
}
