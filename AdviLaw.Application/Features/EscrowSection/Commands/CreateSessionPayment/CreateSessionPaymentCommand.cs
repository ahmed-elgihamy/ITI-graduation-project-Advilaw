using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.EscrowSection.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.Commands.CreateSessionPayment
{
    public class CreateSessionPaymentCommand : IRequest<Response<CreatedEscrowDTO>>
    {

        public int JobId { get; set; }
        public int AppointmentId { get; set; }
        public string ClientId { get; set; } = string.Empty;
    }
}
