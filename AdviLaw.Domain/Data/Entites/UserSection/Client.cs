using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.JobSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.SessionSection;
using server.Data.Entites.SessionUtilities.MessageSection;
using server.Data.Entites.SessionUtilities.ReportSection;
using server.Data.Entites.SessionUtilities.ReviewSection;

namespace server.Data.Entites.UserSection
{
    public class Client : User
    {
        // Navigation Properties


        //Job Section
        public List<Job> Jobs { get; set; } = new();

        //Session Section
        public List<Session> Sessions { get; set; } = new();


        // old code

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
