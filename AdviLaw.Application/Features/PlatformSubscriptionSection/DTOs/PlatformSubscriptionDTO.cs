using AdviLaw.Application.Features.SubscriptionPointSection.DTOs;

namespace AdviLaw.Application.Features.PlatformSubscriptionSection.DTOs
{
    public class PlatformSubscriptionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; } = false;

        public List<SubscriptionPointDTO> Details { get; set; } = new();

    }
}
