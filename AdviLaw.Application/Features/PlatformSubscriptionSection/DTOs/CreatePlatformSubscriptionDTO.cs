using AdviLaw.Application.Features.SubscriptionPointSection.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class CreatePlatformSubscriptionDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }

        public List<CreateSubscriptionPointDTO> Details { get; set; } = new();
    }
}
