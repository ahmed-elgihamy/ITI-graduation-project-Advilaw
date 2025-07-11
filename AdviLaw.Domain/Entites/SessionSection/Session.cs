using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReportSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SessionSection
{
    public class Session
    {
        public int Id { get; set; }
        public SessionStatus Status { get; set; } 

        //Navigation Properties

        //Job
        public int JobId { get; set; }
        public Job Job { get; set; } 

        //Client
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //Lawyer
        public int LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }

        //EscrowTransaction
        public int EscrowTransactionId { get; set; }
        public EscrowTransaction EscrowTransaction { get; set; }

        //Payment
        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        //Messages
        public List<Message>? Messages { get; set; }
        //Reviews
        public List<Review>? Reviews { get; set; }
        //Reports
        public List<Report>? Reports { get; set; }
    }
}
