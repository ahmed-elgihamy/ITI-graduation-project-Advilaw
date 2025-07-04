using AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule;
using AdviLaw.Domain.Entites.AppointmentSection;
using AutoMapper;

namespace AdviLaw.Application.Features.AppointmentSection.DTOs
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDetailsDTO>().ReverseMap();
            CreateMap<CreateAppointmentCommand, Appointment>();
        }
    }
}
