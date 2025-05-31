using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.UserSection;
using System.ComponentModel.DataAnnotations;

namespace server.Data.Entites.JobSection
{
    public class LawyerJobField
    {
        [Key]
        public int LawyerId { get; set; }
        [Key]
        public int JobFieldId { get; set; }

        //Navigation Properties
        public Lawyer Lawyer { get; set; } = new();
        public JobField JobField { get; set; } = new();

    }
}
