using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.LawyerSection.DTOs.Profiling
{
    public class LawyerProfile : Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerListDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Country))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ReverseMap();
        }
    }
}
