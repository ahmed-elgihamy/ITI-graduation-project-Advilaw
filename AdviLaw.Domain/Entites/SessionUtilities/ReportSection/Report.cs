

using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SessionUtilities.ReportSection
{
    public class Report
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; }       // From 1 to 10
        public ReportType Type { get; set; } = ReportType.ClientToLawyer;


        //Navigation Properties
        public int SessionId { get; set; }
        public Session Session { get; set; } = new();

        public string? SenderId { get; set; } 
        public User? Sender { get; set; } 

        public string? ReceiverId { get; set; } 
        public User? Receiver { get; set; } 
    }
}
