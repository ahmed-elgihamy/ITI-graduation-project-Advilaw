using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ScheduleSection;

namespace AdviLaw.Domain.Entites.AppointmentSection
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; } = ScheduleStatus.None;
        public int JobId { get; set; }
        public Job Job { get; set; } 
        public int? ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
    }
}
