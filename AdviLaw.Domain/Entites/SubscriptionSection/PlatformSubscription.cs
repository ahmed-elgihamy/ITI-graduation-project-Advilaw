namespace AdviLaw.Domain.Entites.SubscriptionSection
{
    public class PlatformSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Points { get; set; }

        //Navigation Properties
        public List<SubscriptionPoint> Details { get; set; } = new();
        public List<UserSubscription> UsersSubscriptions { get; set; } = new();
    }
}
