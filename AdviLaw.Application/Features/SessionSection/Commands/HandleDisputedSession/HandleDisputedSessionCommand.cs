using AdviLaw.Application.Basics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SessionSection.Commands.HandleDisputedSession
{
    public class HandleDisputedSessionCommand : IRequest<Response<bool>>
    {
        public int SessionId { get; set; }
        public string CausedBy { get; set; } = string.Empty;
    }
}
