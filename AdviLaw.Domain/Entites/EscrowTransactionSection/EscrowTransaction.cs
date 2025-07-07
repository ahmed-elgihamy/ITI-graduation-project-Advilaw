using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.EscrowTransactionSection
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
        public string? SenderId { get; set; }
        public User? Sender { get; set; }

        //Job Section
        public int JobId { get; set; }
        public Job Job { get; set; }

        //Session Section
        public int? SessionId { get; set; }  // Optional, for when the session is created
        public Session? Session { get; set; }

        // Stripe Session
        public string? StripeSessionId { get; set; } // Stores the Stripe session ID

        //Payment Section
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }
    }
}
