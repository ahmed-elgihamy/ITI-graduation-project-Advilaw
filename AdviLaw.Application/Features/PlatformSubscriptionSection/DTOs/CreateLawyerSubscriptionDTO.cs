using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class CreateLawyerSubscriptionDTO
    {
        public string LawyerId { get; set; } = string.Empty;
        public List<SingleSubscriptionDTO> Subscriptions { get; set; } = new();
    }
}
