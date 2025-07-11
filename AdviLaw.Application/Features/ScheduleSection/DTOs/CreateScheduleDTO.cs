namespace AdviLaw.Application.Features.Schedule.DTOs
{
    public class CreateScheduleDTO
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
