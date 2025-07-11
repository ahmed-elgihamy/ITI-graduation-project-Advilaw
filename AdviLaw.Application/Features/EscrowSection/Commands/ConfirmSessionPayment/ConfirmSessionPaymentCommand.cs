using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.Commands.ConfirmSessionPayment
{
    public class ConfirmSessionPaymentCommand
        : IRequest<Response<int>>
    {
        public string StripeSessionId { get; set; } = string.Empty;
        public int? SessionId { get; set; }
    }
}
