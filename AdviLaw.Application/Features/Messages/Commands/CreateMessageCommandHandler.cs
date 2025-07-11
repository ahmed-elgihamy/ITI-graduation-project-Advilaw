using AdviLaw.Application.Features.Messages.Commands;
using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Repositories;
using MediatR;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
{
    private readonly IMessageRepository _messageRepository;

    public CreateMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($" CreateMessageCommand: SessionId={request.SessionId}, SenderId={request.SenderId}, ReceiverId={request.ReceiverId}, Text={request.Text}");

        var message = new Message
        {
            SessionId = request.SessionId,
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Text = request.Text ?? request.Text ?? string.Empty,
            SentAt = DateTime.UtcNow,
        };

        await _messageRepository.AddAsync(message, cancellationToken);
        return message.Id;
    }
}
