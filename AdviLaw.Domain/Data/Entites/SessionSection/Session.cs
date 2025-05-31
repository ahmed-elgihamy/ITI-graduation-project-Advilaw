using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.JobSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.SessionUtilities.MessageSection;
using server.Data.Entites.SessionUtilities.ReportSection;
using server.Data.Entites.SessionUtilities.ReviewSection;
using server.Data.Entites.UserSection;

namespace server.Data.Entites.SessionSection
{
    public class Session
    {
        public int Id { get; set; }
        public SessionStatus Status { get; set; } = new();

        //Navigation Properties

        //Job
        public int JobId { get; set; }
        public Job Job { get; set; } = new();

        //Client
        public int ClientId { get; set; }
        public Client Client { get; set; } = new();

        //Lawyer
        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; } = new();

        //EscrowTransaction
        public int EscrowTransactionId { get; set; }
        public EscrowTransaction EscrowTransaction { get; set; } = new();

        //Payment
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; } = new();

        //Messages
        public List<Message>? Messages { get; set; } = new();
        //Reviews
        public List<Review>? Reviews { get; set; } = new();
        //Reports
        public List<Report>? Reports { get; set; } = new();
    }
}
