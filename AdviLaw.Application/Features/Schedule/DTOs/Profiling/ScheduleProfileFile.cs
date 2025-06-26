using AutoMapper;
using AdviLaw.Domain.Entites.ScheduleSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Schedule.DTOs.Profiling
{
    public class ScheduleProfileFile : Profile
    {
        public ScheduleProfileFile()
        {
            CreateMap<Domain.Entites.ScheduleSection.Schedule, ScheduleDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
