

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
        public decimal Amount { get; set; }


        //Navigation Properties
        public string? SenderId { get; set; }
        public User? Sender { get; set; }

        public string? ReceiverId { get; set; }
        public User? Receiver { get; set; }

        public int? EscrowTransactionId { get; set; }
        public EscrowTransaction? EscrowTransaction { get; set; }

        public int? SessionId { get; set; }
        public Session? Session { get; set; }

        public int? UserSubscriptionId { get; set; }
        public UserSubscription? UserSubscription { get; set; }
    }
}
