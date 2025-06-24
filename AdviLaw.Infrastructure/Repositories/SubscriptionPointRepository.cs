using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;

namespace AdviLaw.Infrastructure.Repositories
{
    public class SubscriptionPointRepository(AdviLawDBContext dbContext) : GenericRepository<SubscriptionPoint>(dbContext), ISubscriptionPointRepository
    {}
}
