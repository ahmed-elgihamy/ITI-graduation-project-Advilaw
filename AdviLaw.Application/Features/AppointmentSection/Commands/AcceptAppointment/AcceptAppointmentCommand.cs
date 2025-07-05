using AdviLaw.Application.Basics;
using AdviLaw.Domain.Entities.UserSection;
using MediatR;

namespace AdviLaw.Application.Features.AppointmentSection.Commands.AcceptAppointment
{
    public class AcceptAppointmentCommand : IRequest<Response<bool>>
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
