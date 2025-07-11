using AdviLaw.Application.Features.Messages.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Messages.Query
{
    public class GetSessionMessagesQuery : IRequest<List<ChatMessageDto>>
    {
        public int SessionId { get; set; }

        public GetSessionMessagesQuery(int sessionId)
        {
            SessionId = sessionId;
        }
    }
}
