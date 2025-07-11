using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.Messages.Query
{
    using AdviLaw.Application.Features.Messages.DTOs;
    using AdviLaw.Domain.Repositories;
    using MediatR;

    public class GetSessionMessagesHandler : IRequestHandler<GetSessionMessagesQuery, List<ChatMessageDto>>
    {
        private readonly IMessageRepository _messageRepo;

        public GetSessionMessagesHandler(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public async Task<List<ChatMessageDto>> Handle(GetSessionMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepo.GetBySessionIdAsync(request.SessionId, cancellationToken);

            return messages.Select(m => new ChatMessageDto
            {
                Id = m.Id,
                SessionId = m.SessionId,
                SenderId = m.SenderId,
                Text = m.Text,
                SentAt = m.SentAt
            }).ToList();
        }
    }

}
