using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.AdminSection.DTOs
{
    public class AdminProfileMappingProfile : Profile
    {
        public AdminProfileMappingProfile()
        {
            CreateMap<Admin, AdminProfileDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role.ToString()))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.User.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.User.Country))
                .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.User.CountryCode))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.User.PostalCode))
                .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User.NationalityId))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.User.Gender.ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.User.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.User.UpdatedAt));
        }
    }
} 