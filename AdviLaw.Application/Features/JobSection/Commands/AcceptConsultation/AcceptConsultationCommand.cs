using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.AcceptConsultation
{
    public class AcceptConsultationCommand : IRequest<Response<bool>>
    {
        public int JobId { get; set; }
        public int LawyerId { get; set; }
    }
}
