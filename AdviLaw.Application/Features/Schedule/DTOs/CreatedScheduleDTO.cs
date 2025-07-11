namespace AdviLaw.Application.Features.Schedule.DTOs
{
    public class CreatedScheduleDTO
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string UserId { get; set; }
    }
}
