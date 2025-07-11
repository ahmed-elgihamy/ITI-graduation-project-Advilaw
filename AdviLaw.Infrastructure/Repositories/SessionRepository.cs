using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly AdviLawDBContext _dbContext;

        public SessionRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session?> GetSessionWithClientAndLawyerAsync(int sessionId)
        {
            return await _dbContext.Sessions
                         .Include(s => s.Client).ThenInclude(c => c.User)
                         .Include(s => s.Lawyer).ThenInclude(l => l.User)
                         .Include(s => s.Job)
                         .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

  


    }
}
