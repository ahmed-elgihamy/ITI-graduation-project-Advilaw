using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
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
    }
}
