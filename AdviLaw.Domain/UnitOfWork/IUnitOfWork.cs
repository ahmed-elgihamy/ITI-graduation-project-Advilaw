using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;

namespace AdviLaw.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Generic repositories  
        IGenericRepository<Lawyer> GenericLawyers { get; }
        IGenericRepository<Client> GenericClients { get; }
        IGenericRepository<Admin> GenericAdmins { get; }

        // Specialized repositories  
        IJobFieldRepository JobFields { get; }
        ILawyerRepository Lawyers { get; }
        IJobRepository Jobs { get; }
        IPlatformSubscriptionRepository PlatformSubscriptions { get; }
        ISubscriptionPointRepository SubscriptionPoints { get; }
        IUserSubscriptionRepository UserSubscriptions { get; }
        IPaymentRepository Payments { get; }
        IReviewRepository Reviews { get; }
        IScheduleRepository Schedules { get; }
        Task<int> SaveChangesAsync();
        void Update<T>(T entity) where T : class;
        IRefreshTokenRepository RefreshTokens { get; }
   

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
