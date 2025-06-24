using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;

namespace AdviLaw.Infrastructure.Repositories
{
    public class UserSubscriptionRepository(AdviLawDBContext dbContext) : GenericRepository<UserSubscription>(dbContext), IUserSubscriptionRepository
    {
    }
}
