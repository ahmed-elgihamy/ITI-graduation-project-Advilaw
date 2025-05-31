using server.Data.Entites.JobSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.SessionSection;
using server.Data.Entites.UserSection;

namespace server.Data.Entites.EscrowTransactionSection
{
    public class EscrowTransaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public CurrencyType Currency { get; set; } = CurrencyType.EGP; // or EGP,USD etc.
        public EscrowTransactionStatus Status { get; set; } = EscrowTransactionStatus.Pending;
        public EscrowTransactionType Type { get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Stripe;
        public string? TransferId { get; set; } // Payment Provider IDs (Stripe, PayPal, etc.)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReleasedAt { get; set; } // When escrow is completed

        // Navigation Properties

        //User Section
        public int? SenderId { get; set; }
        public User Sender { get; set; } = default!;

        //Job Section
        public int JobId { get; set; }
        public Job Job { get; set; } = default!;

        //Session Section
        public int? SessionId { get; set; }  // Optional, for when the session is created
        public Session? Session { get; set; }

        //Payment Section
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; } = default!;
    }
}
