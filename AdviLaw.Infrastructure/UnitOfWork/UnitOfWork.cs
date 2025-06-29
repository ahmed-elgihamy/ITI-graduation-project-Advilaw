using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AdviLawDBContext _dbContext;

        public IGenericRepository<Lawyer> GenericLawyers { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IJobFieldRepository JobFields { get; }
        public ILawyerRepository Lawyers { get; }
        public IJobRepository Jobs { get; }
        public IGenericRepository<Client> GenericClients { get; }
        public IPlatformSubscriptionRepository PlatformSubscriptions { get; }
        public ISubscriptionPointRepository SubscriptionPoints { get; }
        public IUserSubscriptionRepository UserSubscriptions { get; }
        public IPaymentRepository Payments { get; }

        public IGenericRepository<Admin> GenericAdmins { get; }

        public IScheduleRepository Schedules { get; }
         public IReviewRepository Reviews { get; }

        public UnitOfWork(AdviLawDBContext dbContext)
        {
            _dbContext = dbContext;

            GenericLawyers = new GenericRepository<Lawyer>(_dbContext);
            RefreshTokens = new RefreshTokenRepository(_dbContext);
            JobFields = new JobFieldRepository(_dbContext);
            Lawyers = new LawyerRepository(_dbContext);
            Jobs = new JobRepository(_dbContext);
            GenericClients = new GenericRepository<Client>(_dbContext);
            PlatformSubscriptions = new PlatformSubscripitonRepository(_dbContext);
            SubscriptionPoints = new SubscriptionPointRepository(_dbContext);
            UserSubscriptions = new UserSubscriptionRepository(_dbContext);
            Payments = new PaymentRepository(_dbContext);

            GenericAdmins = new GenericRepository<Admin>(_dbContext);

            Reviews = new ReviewRepository(_dbContext);
            Schedules = new ScheduleRepository(_dbContext);

        }


        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);


        public void Update<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
