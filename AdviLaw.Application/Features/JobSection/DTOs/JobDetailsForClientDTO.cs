using AdviLaw.Application.Features.AppointmentSection.DTOs;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Domain.Entites.AppointmentSection;
using AdviLaw.Domain.Entites.JobSection;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class JobDetailsForClientDTO
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Budget { get; set; }
        public JobStatus Status { get; set; } = JobStatus.NotAssigned;
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;
        public string StatusLabel { get; set; }

        //Navigation Properties
        public int JobFieldId { get; set; }
        public string JobFieldName { get; set; } = string.Empty;  // mapped
        public int? LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty; // mapped
        public string LawyerProfilePictureUrl { get; set; } = string.Empty; // mapped
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;  // mapped
        public string ClientProfilePictureUrl { get; set; } = string.Empty; // mapped


        public int? EscrowTransactionId { get; set; }

        public int? SessionId { get; set; }

        public List<ProposalListDTO>? Proposals { get; set; }
        public List<AppointmentDetailsDTO>? Appointments { get; set; }
    }
}
