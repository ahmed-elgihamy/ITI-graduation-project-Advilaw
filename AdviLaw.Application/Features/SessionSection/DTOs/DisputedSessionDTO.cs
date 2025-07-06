using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SessionSection.DTOs
{
    public class DisputedSessionDTO
    {
        public int SessionId { get; set; }
        public string CausedBy { get; set; } = string.Empty;
    }
}
