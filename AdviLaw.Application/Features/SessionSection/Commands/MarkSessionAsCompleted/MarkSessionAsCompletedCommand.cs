using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.SessionSection.Commands.MarkSessionAsCompleted
{
    using MediatR;

  
        public class MarkSessionAsCompletedCommand : IRequest<bool>
        {
            public int SessionId { get; set; }

            public MarkSessionAsCompletedCommand(int sessionId)
            {
                SessionId = sessionId;
            }
        }
    }


