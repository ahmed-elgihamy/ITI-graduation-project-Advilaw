using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.UserSubscriptionSection.DTOs
{
    public class UserSubscriptionDetailsDTO
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Country { get; set; } = string.Empty;
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
