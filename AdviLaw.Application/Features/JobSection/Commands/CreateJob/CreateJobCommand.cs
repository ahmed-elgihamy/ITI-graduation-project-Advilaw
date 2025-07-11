

using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.JobSection.DTOs;
using AdviLaw.Domain.Entites.JobSection;
using MediatR;

namespace AdviLaw.Application.Features.JobSection.Commands.CreateJob
{
    public class CreateJobCommand : IRequest<Response<CreatedJobDTO>>
    {
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Budget { get; set; }
        public JobType Type { get; set; }   
        public bool IsAnonymus { get; set; } = false;

        //Navigation Properties
        public int JobFieldId { get; set; }
        public int? LawyerId { get; set; }
        public int? ClientId { get; set; }
        public string? UserId { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public double? DurationHours { get; set; }

    }
}
