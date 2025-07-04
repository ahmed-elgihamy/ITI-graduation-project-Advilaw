using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.ProposalSection
{
    public class Proposal
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Budget { get; set; }
        public ProposalStatus Status { get; set; }

        //Navigation Properties
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int? LawyerId { get; set; }
        public Lawyer? Lawyer { get; set; }
    }
}
