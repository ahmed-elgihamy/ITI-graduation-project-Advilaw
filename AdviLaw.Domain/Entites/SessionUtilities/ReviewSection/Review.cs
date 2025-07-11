
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SessionUtilities.ReviewSection
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; }       // From 1 to 10
        public ReviewType Type { get; set; } = ReviewType.ClientToLawyer;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //Navigation Properties
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public string? ReviewerId { get; set; }
        public User? Reviewer { get; set; }

        public string? RevieweeId { get; set; }
        public User? Reviewee { get; set; }

    }
}
