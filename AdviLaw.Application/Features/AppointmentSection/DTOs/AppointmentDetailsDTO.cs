using AdviLaw.Domain.Entites.ScheduleSection;

namespace AdviLaw.Application.Features.AppointmentSection.DTOs
{
    public class AppointmentDetailsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }  
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; }
        public int JobId { get; set; }
        public int? ScheduleId { get; set; }

        // Extra for UI:
        public string JobHeader { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string LawyerName { get; set; } = string.Empty;
    }
}
