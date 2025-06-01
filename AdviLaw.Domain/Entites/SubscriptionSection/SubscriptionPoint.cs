namespace AdviLaw.Domain.Entites.SubscriptionSection
{
    public class SubscriptionPoint
    {
        public int Id { get; set; }
        public string Point { get; set; } = string.Empty;

        //Navigation Properties
        public PlatformSubscription PlatformSubscription { get; set; } = new();
    }
}
