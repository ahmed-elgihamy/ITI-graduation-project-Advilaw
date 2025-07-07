using AdviLaw.Application.Features.AppointmentSection.DTOs;
using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Domain.Entites.AppointmentSection;
using AutoMapper;

namespace AdviLaw.Application.Features.AppointmentSection.DTOs
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDetailsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
                .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId))
                
                .ForMember(dest => dest.JobHeader, opt => opt.Ignore())
                .ForMember(dest => dest.ClientName, opt => opt.Ignore())
                .ForMember(dest => dest.LawyerName, opt => opt.Ignore());

            CreateMap<CreateAppointmentCommand, Appointment>()
                .ForMember(dest => dest.ScheduleId, opt => opt.MapFrom(src => src.ScheduleId == 0 ? null : src.ScheduleId));
        }
    }
}