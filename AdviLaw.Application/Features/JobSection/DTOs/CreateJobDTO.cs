using AdviLaw.Domain.Entites.JobSection;

namespace AdviLaw.Application.Features.JobSection.DTOs
{
    public class CreateJobDTO
    {
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Budget { get; set; }
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;

        //Navigation Properties
        public int JobFieldId { get; set; }
        public int? LawyerId { get; set; }

        public DateTime? AppointmentTime { get; set; }
        public double? DurationHours { get; set; }
    }
}
