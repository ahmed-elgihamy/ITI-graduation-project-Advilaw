
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SubscriptionSection
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }

        //Navigation Properties
        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; } = new();

        public int SubscriptionTypeId { get; set; }
        public PlatformSubscription SubscriptionType { get; set; } = new();

        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = new();


    }
}
