using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;

namespace AdviLaw.Infrastructure.Repositories
{
    public class LawyerRepository : GenericRepository<Lawyer>, ILawyerRepository
    {
        public LawyerRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
        }
    }
}
