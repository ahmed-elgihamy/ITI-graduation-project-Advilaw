using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<CreateUserCommand,User>();
        }
    }
}