using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Messages.Commands
{
    public class CreateMessageCommand : IRequest<Guid>
    {
        public int SessionId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ReceiverId { get; set; }
    }
}

