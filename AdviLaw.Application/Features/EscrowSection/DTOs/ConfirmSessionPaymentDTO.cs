using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.DTOs
{
    public class ConfirmSessionPaymentDTO
    {
        public string StripeSessionId { get; set; } = string.Empty;

        public int? SessionId { get; set; }
        public int? PaymentId { get; set; }
    }
}


