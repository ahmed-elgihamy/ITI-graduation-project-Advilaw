

using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SessionUtilities.MessageSection
{
    public class Message
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;

        public string SenderId { get; set; } = string.Empty;
        public User Sender { get; set; } = null!;

        public string? ReceiverId { get; set; }
        public User? Receiver { get; set; }

        public string Text { get; set; } = string.Empty;
        public MessageType Type { get; set; } = MessageType.ClientToLawyer;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
