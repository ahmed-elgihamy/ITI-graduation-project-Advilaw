namespace AdviLaw.Application.Features.UserSubscriptionSection.DTOs
{
    public class LawyerSubscriptionListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // mapped
        public decimal Price { get; set; } // mapped
        public int Points { get; set; } // mapped
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
