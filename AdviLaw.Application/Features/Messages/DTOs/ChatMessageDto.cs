using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Messages.DTOs
{
    public class ChatMessageDto
    {
        public Guid Id { get; set; }
        public int SessionId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }

}
