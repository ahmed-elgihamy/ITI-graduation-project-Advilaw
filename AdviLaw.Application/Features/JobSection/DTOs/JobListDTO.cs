using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class JobListDTO
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int budget { get; set; }
        public bool IsAnonymus { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientImageUrl { get; set; }
        public int JobFieldId { get; set; }
        public string JobFieldName { get; set; }
        public string Status { get; set; }
        public int Type { get; set; }
        public int? Duration { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public string LawyerName { get; set; }
    }
}