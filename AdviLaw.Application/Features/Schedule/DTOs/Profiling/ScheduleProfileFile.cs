using AdviLaw.Application.Features.Schedule.DTOs;
using AdviLaw.Domain.Entites.ScheduleSection;
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
