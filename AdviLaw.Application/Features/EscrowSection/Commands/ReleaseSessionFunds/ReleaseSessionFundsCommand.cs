using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.EscrowSection.Commands.ReleaseSessionFunds
{
    public class ReleaseSessionFundsCommand : IRequest<Response<bool>>
    {
        public int SessionId { get; set; }
    }
}
