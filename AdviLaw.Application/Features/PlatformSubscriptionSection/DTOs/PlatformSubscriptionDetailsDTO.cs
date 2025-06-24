using AdviLaw.Application.Features.SubscriptionPointSection.DTOs;
using AdviLaw.Application.Features.UserSections.DTOs;
using AdviLaw.Application.Features.UserSubscriptionSection.DTOs;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class PlatformSubscriptionDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }

        public List<SubscriptionPointDTO> Details { get; set; } = new();
        public List<UserSubscriptionDetailsDTO> UsersSubscriptions { get; set; } = new();
    }
}
