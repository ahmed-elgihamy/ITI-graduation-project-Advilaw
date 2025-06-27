using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Schedule.DTOs
{
 public class LawyerScheduleDTO
    {
        public string Day { get; set; } = string.Empty;

        public List<TimeRangeDTO> TimeRanges { get; set; } = new();
    }
}
