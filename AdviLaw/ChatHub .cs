using AdviLaw.Application.Features.Messages.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace AdviLaw
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task JoinSession(int sessionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Session_{sessionId}");
        }

        public async Task SendMessage(int sessionId, string senderId, string content, string? receiverId)
        {
            var command = new CreateMessageCommand
            {
                SessionId = sessionId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Text = content
            };

            await _mediator.Send(command);

            await Clients.Group($"Session_{sessionId}").SendAsync("ReceiveMessage", new
            {
                sessionId,
                senderId,
                receiverId,
                content,
                sentAt = DateTime.UtcNow
            });
        }

    }
}
