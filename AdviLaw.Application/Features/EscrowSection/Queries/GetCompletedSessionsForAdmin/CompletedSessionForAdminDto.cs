namespace AdviLaw.Application.Features.EscrowSection.Queries.GetCompletedSessionsForAdmin
{
    public class CompletedSessionForAdminDto
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string JobTitle { get; set; }
        public string LawyerName { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
} 