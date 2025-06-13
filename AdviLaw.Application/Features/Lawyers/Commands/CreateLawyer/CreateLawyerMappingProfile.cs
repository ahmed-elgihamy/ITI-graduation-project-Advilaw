using AdviLaw.Application.DTOs.Lawyer;
using AdviLaw.Domain.Entities.UserSection;

using AutoMapper;

namespace AdviLaw.Application.Features.Lawyers.Commands.CreateLawyer
{
    public class CreateLawyerMappingProfile : Profile
    {
        public CreateLawyerMappingProfile()
        {
            CreateMap<CreateLawyerCommand, Lawyer>()

            // wait for admin review to be approved
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => false))
                .ReverseMap();

            CreateMap<Lawyer, CreateLawyerDto>();



        }
    }
} 