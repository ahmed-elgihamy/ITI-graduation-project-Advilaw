

using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.JobSection
{
    public class Job
    {
        public int Id { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int budget { get; set; }
        public JobStatus Status { get; set; } = JobStatus.NotAssigned;
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;

        //Navigation Properties
        public int JobFieldId { get; set; }
        public JobField JobField { get; set; } = new();

        public int? LawyerId { get; set; }
        public Lawyer? Lawyer { get; set; } = new();

        public int ClientId { get; set; }
        public Client Client { get; set; } = new();

        public int? EscrowTransactionId { get; set; }
        public EscrowTransaction? EscrowTransaction { get; set; } = new();

        public int? SessionId { get; set; }
        public Session? Session { get; set; } = new();

        public List<Proposal>? Proposals { get; set; } = new();
    }
}
