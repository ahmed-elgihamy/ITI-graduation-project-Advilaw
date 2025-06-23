using AdviLaw.Domain.Entites.JobSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class CreatedJobDTO
    {
        public int? Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int budget { get; set; }
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;

        //Navigation Properties
        public int JobFieldId { get; set; }
        public int? LawyerId { get; set; }
    }
}
