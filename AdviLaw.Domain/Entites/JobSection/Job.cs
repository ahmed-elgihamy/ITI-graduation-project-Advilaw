using AdviLaw.Domain.Entites.AppointmentSection;
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
        public int Budget { get; set; }
        public JobStatus Status { get; set; } = JobStatus.NotAssigned;
        public JobType Type { get; set; }
        public bool IsAnonymus { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        //Navigation Properties
        public int JobFieldId { get; set; }
        public JobField JobField { get; set; }

        public int? LawyerId { get; set; }
        public Lawyer? Lawyer { get; set; }  // لا تهيئة هنا

        public int? ClientId { get; set; }
        public Client? Client { get; set; }

        public int? EscrowTransactionId { get; set; }
        public EscrowTransaction? EscrowTransaction { get; set; }  // لا تهيئة

        public int? SessionId { get; set; }
        public Session? Session { get; set; }  // لا تهيئة

        public List<Proposal>? Proposals { get; set; }

        public DateTime? AppointmentTime { get; set; }
        public double? DurationHours { get; set; }

        public List<Appointment>? Appointments { get; set; }

    }
}
