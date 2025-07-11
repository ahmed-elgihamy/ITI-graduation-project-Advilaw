using AdviLaw.Domain.Entities.UserSection;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.LawyerProfile.DTOs.Profiling
{
    public class LawyerProfileMapping : Profile
    {
        public LawyerProfileMapping()
        {
            CreateMap<Lawyer, LawyerProfileDTO>()
  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) 

    .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User!.UserName))
    .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.User!.ImageUrl))
    .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points)); // Map Points

        }
    }
}

