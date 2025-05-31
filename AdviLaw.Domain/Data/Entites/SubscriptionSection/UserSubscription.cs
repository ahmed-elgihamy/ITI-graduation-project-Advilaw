using server.Data.Entites.PaymentSection;
using server.Data.Entites.UserSection;

namespace server.Data.Entites.SubscriptionSection
{
    public class UserSubscription
    {
        public int Id { get; set; }
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
        public DateTime StartDate { get; set; }
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
