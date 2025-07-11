using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.ScheduleSection
{
    public class Schedule
    {
        public int Id { get; set; }
        //public string Content { get; set; } = string.Empty;

        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }         
        public TimeSpan EndTime { get; set; }           
        //public ScheduleType Type { get; set; }
        //public ScheduleStatus Status { get; set; }

        //Navigation Properties
        //public int JobId { get; set; }
        //public Job Job { get; set; } = new();

        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
