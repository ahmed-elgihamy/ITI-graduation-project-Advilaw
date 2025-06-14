using AdviLaw.Application.DTOs.Users;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using AutoMapper;

namespace AdviLaw.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<UserRegisterDto, User>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        }
    }
}