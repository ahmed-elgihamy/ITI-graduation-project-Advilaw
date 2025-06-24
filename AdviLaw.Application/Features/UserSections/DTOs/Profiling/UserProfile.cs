using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;

namespace AdviLaw.Application.Features.UserSections.DTOs.Profiling
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListDTO>()
                .ReverseMap();
        }
    }
}
