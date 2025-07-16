using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AdviLaw.Application.Features.SessionSection.Commands.MarkSessionAsCompletedCommand
{
    public class MarkSessionAsCompletedCommand: IRequest<bool>
    {
        public int SessionId { get; set; }
        public MarkSessionAsCompletedCommand(int sessionId)
        {
            SessionId = sessionId;
        }
    }
}
