using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.RejectConsultation
{
    public class RejectConsultationCommand : IRequest<Response<bool>>
    {
        public int JobId { get; set; }
        public int LawyerId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}