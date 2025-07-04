using AdviLaw.Application.DTOs.Lawyer;
using AdviLaw.Application.DTOs.Users;
using AdviLaw.Domain.Entities.UserSection;

using AutoMapper;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerMappingProfile : Profile
    {
        public CreateLawyerMappingProfile()
        {
          
            // wait for admin review to be approved
               CreateMap<CreateLawyerCommand, Lawyer>()
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Fields, opt => opt.Ignore()); // ✅


            CreateMap<Lawyer, LawyerResponseDto>();
            CreateMap<UserRegisterDto, CreateLawyerCommand>();



        }
    }
} 