using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.SessionUtilities.MessageSection;
using server.Data.Entites.SessionUtilities.ReportSection;
using server.Data.Entites.SessionUtilities.ReviewSection;
using server.Data.Entites.SubscriptionSection;
using server.Data.Tokens.RefreshTokenSection;

namespace server.Data.Entites.UserSection
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsConfirmedEmail { get; set; }
        public string Password { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsConfirmedPhone { get; set; }
        public int NationalityId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public decimal? Balance { get; set; } = 0; // بدل الAccount
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;


        //Navigation Properties

        // Payment Section
        public List<Payment> SentPayments { get; set; } = new();
        public List<Payment> ReceivedPayments { get; set; } = new();
        public List<EscrowTransaction> EscrowTransactions { get; set; } = new();

        //Session => Reviews Section
        public List<Review> SentReviews { get; set; } = new();
        public List<Review> ReceivedReviews { get; set; } = new();

        //Session => Messages Section
        public List<Message> SentMessages { get; set; } = new();
        public List<Message> ReceivedMessages { get; set; } = new();

        //Session => Reports Section
        public List<Report> SentReports { get; set; } = new();
        public List<Report> ReceivedReports { get; set; } = new();

        public List<RefreshToken> RefreshTokens { get; set; } = new();
        
    }
}
