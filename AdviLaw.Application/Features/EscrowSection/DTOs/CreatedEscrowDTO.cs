using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.DTOs
{
    public class CreatedEscrowDTO
    {
        public int EscrowId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "egp";
        public DateTime CreatedAt { get; set; }
    }
}
