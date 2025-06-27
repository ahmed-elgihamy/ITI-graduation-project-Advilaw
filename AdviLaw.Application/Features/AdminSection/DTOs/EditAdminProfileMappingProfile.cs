using AutoMapper;
using AdviLaw.Application.Features.AdminSection.DTOs;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Application.Features.AdminSection.DTOs
{
    public class EditAdminProfileMappingProfile : Profile
    {
        public EditAdminProfileMappingProfile()
        {
            CreateMap<EditAdminProfileDto, User>();

        }
    }
} 