using AdviLaw.Application.Features.ScheduleSection.DTOs;

namespace AdviLaw.Application.Features.Schedule.DTOs
{
 public class LawyerScheduleDTO
    {
        public DayOfWeek Day { get; set; } 

        public List<TimeRangeDTO> TimeRanges { get; set; } = new();
    }
}
