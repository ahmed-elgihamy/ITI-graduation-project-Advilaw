using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.DTOs
{
    public class CreateSessionPaymentDTO
    {
        public int JobId { get; set; }
        public int AppointmentId { get; set; }
        public string ClientId { get; set; } = string.Empty;
    }

}
