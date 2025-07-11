using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<Session?> GetSessionWithClientAndLawyerAsync(int sessionId);
    }
}
