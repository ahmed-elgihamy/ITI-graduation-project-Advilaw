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
        public int Budget { get; set; }
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;
        public int JobFieldId { get; set; }
        public int? LawyerId { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public double? DurationHours { get; set; }
    }

}
