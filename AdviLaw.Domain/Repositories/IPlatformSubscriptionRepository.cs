using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.IGenericRepo;

namespace AdviLaw.Domain.Repositories
{
    public interface IPlatformSubscriptionRepository : IGenericRepository<PlatformSubscription>
    {
        Task<PlatformSubscription> GetByIdDetails(int id);
    }
}
