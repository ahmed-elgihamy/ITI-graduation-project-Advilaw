

using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.PaymentSection
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; }

        //Navigation Properties
        public int SenderId { get; set; }
        public User Sender { get; set; } = new();

        public int ReceiverId { get; set; }
        public User Receiver { get; set; } = new();

        public int? EscrowTransactionId { get; set; }
        public EscrowTransaction? EscrowTransaction { get; set; } = new();

        public int? SessionId { get; set; }
        public Session? Session { get; set; } = new();

        public int UserSubscriptionId { get; set; }
        public UserSubscription UserSubscription { get; set; } = new();
    }
}
