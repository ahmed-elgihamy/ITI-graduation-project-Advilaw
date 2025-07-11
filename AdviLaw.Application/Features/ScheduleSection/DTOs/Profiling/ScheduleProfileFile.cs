using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Domain.Entites.ScheduleSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Application.Features.ScheduleSection.DTOs;
using AutoMapper;


public class ScheduleProfileFile : Profile
{
    public ScheduleProfileFile()
    {
        CreateMap<Schedule, ScheduleDTO>();
        //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
        //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateScheduleDTO, Schedule>().ReverseMap();
        CreateMap<Schedule, CreatedScheduleDTO>().ReverseMap();
    }
}
