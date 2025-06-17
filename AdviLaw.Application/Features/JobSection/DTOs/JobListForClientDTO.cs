using AdviLaw.Domain.Entites.JobSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class JobListForClientDTO
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int budget { get; set; }
        public JobStatus Status { get; set; } = JobStatus.NotAssigned;
        public int JobFieldId { get; set; }
        public string JobFieldName { get; set; } = string.Empty;
        public int? LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public string LawyerImageUrl { get; set; } = string.Empty;
        public bool IsAnonymus { get; set; } = false;
    }
}
