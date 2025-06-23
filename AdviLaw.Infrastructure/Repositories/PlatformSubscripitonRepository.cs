using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Repositories
{
    public class PlatformSubscripitonRepository(AdviLawDBContext dbContext) : GenericRepository<PlatformSubscription>(dbContext), IPlatformSubscriptionRepository
    {
        private readonly AdviLawDBContext db = dbContext;
        public async Task<PlatformSubscription> GetByIdDetails(int id)
        {
            var platformSubscriptions = await db.PlatformSubscriptions
                .Include(ps => ps.UsersSubscriptions)
                .ThenInclude(us => us.Lawyer)
                .Include(ps => ps.Details)
                .FirstOrDefaultAsync(ps => ps.Id == id);

            return platformSubscriptions;
        }
    }
}
