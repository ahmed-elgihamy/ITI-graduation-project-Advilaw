using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AdviLawDBContext _context;

        public MessageRepository(AdviLawDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message, CancellationToken cancellationToken)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
