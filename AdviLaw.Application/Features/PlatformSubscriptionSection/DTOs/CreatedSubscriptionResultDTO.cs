using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class CreatedSubscriptionResultDTO
    {
        public string SubscriptionName { get; set; } = string.Empty;
        public int SubscriptionTypeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

