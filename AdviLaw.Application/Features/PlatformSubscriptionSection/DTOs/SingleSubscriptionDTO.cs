using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class SingleSubscriptionDTO
    {
        public int SubscriptionTypeId { get; set; }
        public decimal Amount { get; set; }
        public string SubscriptionName { get; set; } = string.Empty;
    }
}
