

using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.SessionUtilities.MessageSection
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public MessageType Type { get; set; } = MessageType.ClientToLawyer;


        //Navigation Properties
        public int SessionId { get; set; }
        public Session Session { get; set; } = new();

        public string? SenderId { get; set; }
        public User? Sender { get; set; }

        public string? ReceiverId { get; set; }
        public User? Receiver { get; set; }
    }
}
