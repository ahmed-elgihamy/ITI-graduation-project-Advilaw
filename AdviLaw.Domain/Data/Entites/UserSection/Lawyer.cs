using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.JobSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.ProposalSection;
using server.Data.Entites.SessionSection;
using server.Data.Entites.SessionUtilities.MessageSection;
using server.Data.Entites.SessionUtilities.ReportSection;
using server.Data.Entites.SessionUtilities.ReviewSection;
using server.Data.Entites.SubscriptionSection;

namespace server.Data.Entites.UserSection
{
    public class Lawyer : User
    {
        public string ProfileHeader { get; set; } = string.Empty;
        public string ProfileAbout { get; set; } = string.Empty;
        public int LawyerCardID { get; set; }

        //Navigation Properties

        //Fields
        public List<LawyerJobField> Fields { get; set; } = new();

        //Job Section
        public List<Job> Jobs { get; set; } = new();
        public List<Proposal> Proposals { get; set; } = new();


        //Session Section
        public List<Session> Sessions { get; set; } = new();

        //UserSubscription
        public List<UserSubscription> UserSubscriptions { get; set; } = new();

        //old code

        ////Session => Reviews Section
        //public List<Review> SentReviews { get; set; } = new();
        //public List<Review> ReceivedReviews { get; set; } = new();

        ////Session => Messages Section
        //public List<Message> SentMessages { get; set; } = new();
        //public List<Message> ReceivedMessages { get; set; } = new();

        ////Session => Reports Section
        //public List<Report> SentReports { get; set; } = new();
        //public List<Report> ReceivedReports { get; set; } = new();

        //// Payment Section
        //public List<EscrowTransaction> EscrowTransactions { get; set; } = new();
    }
}
