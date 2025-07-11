using AdviLaw.Application.Features.LawyerSection.Commands.UpdateLawyerProfile;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.LawyerSection.DTOs.Profiling
{
    public class LawyerProfile : Profile
    {
        public LawyerProfile()
        {
            CreateMap<Lawyer, LawyerListDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User!.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User!.Country))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User!.ImageUrl))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User!.Role))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.IsApproved))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ProfileHeader, opt => opt.MapFrom(src => src.ProfileHeader))
                .ReverseMap();

            CreateMap<Lawyer, UpdateLawyerProfileCommand>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User!.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User!.Country))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.User!.CountryCode))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.User!.PostalCode))
                .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User!.NationalityId))
                .ReverseMap();

            CreateMap<Lawyer, LawyerDetailsDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User!.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User!.Country))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User!.Gender))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.User!.CountryCode))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.User!.PostalCode))
                .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User!.NationalityId))
                .ReverseMap();


        }
    }
}
