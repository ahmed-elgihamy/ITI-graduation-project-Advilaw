using AdviLaw.Application.DTOs.Users;
using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<UserRegisterDto, User>()
             .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
}