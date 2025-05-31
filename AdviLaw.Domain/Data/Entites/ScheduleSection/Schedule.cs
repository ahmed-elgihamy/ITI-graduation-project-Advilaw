using server.Data.Entites.JobSection;

namespace server.Data.Entites.ScheduleSection
{
    public class Schedule
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public ScheduleType Type { get; set; }
        public ScheduleStatus Status { get; set; }

        //Navigation Properties
        public int JobId { get; set; }
        public Job Job { get; set; } = new();
    }
}
