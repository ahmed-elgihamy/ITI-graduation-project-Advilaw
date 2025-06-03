
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


        //Navigation Properties
        public int SessionId { get; set; }
        public Session Session { get; set; } = new();

        public int ReviewerId { get; set; }
        public User Reviewer { get; set; } = new();
        public int RevieweeId { get; set; }
        public User Reviewee { get; set; } = new();

        //كده كده الريفيو مفيهوش غير كلاينت و لويار
        //public int ClientId{ get; set; } 
        //public Client Client{ get; set; } = new();
        //public int LawyerId { get; set; }
        //public Lawyer Lawyer { get; set; } = new();
    }
}
