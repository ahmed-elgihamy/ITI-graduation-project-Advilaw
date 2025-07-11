using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.AdminSection.DTOs
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lawyer, LawyerForAdminDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
                .ForMember(dest => dest.BarCardImagePath, opt => opt.MapFrom(src => src.BarCardImagePath))
                .ForMember(dest => dest.NationalIDImagePath, opt => opt.MapFrom(src => src.NationalIDImagePath))
                .ForMember(dest => dest.BarAssociationCardNumber, opt => opt.MapFrom(src => src.BarAssociationCardNumber))
                .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.User.NationalityId))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.IsApproved))
                .ReverseMap();
        }
    }

}
