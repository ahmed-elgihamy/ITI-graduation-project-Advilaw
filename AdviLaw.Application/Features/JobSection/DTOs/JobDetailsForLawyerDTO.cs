using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class JobDetailsForLawyerDTO
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Budget { get; set; }
        public JobStatus Status { get; set; } = JobStatus.NotAssigned;
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;

        //Navigation Properties
        public int JobFieldId { get; set; }
        public string JobFieldName { get; set; } = string.Empty;  // mapped
        public int? LawyerId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;  // mapped
        public string ClientProfilePictureUrl { get; set; } = string.Empty; // mapped
        public int? EscrowTransactionId { get; set; }
        public int? SessionId { get; set; }
    }
}
