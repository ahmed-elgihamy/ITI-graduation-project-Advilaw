using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.AppointmentSection.DTOs;
using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;

namespace AdviLaw.Application.Features.AppointmentSection.Commands.CreateSchedule
{
    public class CreateAppointmentCommand : IRequest<Response<AppointmentDetailsDTO>>
    {
        public UserRole? UserRole { get; set; }
        public int JobId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime Date { get; set; }
    }
}
