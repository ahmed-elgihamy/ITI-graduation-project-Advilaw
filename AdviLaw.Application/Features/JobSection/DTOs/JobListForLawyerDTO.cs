using AdviLaw.Domain.Entites.JobSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class JobListForLawyerDTO
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int budget { get; set; }
        public bool IsAnonymus { get; set; } = false;
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientImageUrl { get; set; } = string.Empty;
        public int JobFieldId { get; set; }
        public string JobFieldName { get; set; } = string.Empty;
    }
}
