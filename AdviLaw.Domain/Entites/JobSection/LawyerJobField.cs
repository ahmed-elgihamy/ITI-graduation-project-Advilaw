using AdviLaw.Domain.Entities.UserSection;
using System.ComponentModel.DataAnnotations;

namespace AdviLaw.Domain.Entites.JobSection
{
    public class LawyerJobField
    {
        public int LawyerId { get; set; }
        public int JobFieldId { get; set; }

        // Navigation Properties
        public Lawyer Lawyer { get; set; }
        public JobField JobField { get; set; } 
    }
}
