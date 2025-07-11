using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message, CancellationToken cancellationToken);
        Task<List<Message>> GetBySessionIdAsync(int sessionId, CancellationToken cancellationToken);

    }
}
