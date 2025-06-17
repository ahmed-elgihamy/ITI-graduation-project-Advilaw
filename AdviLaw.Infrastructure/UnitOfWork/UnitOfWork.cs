using System;
using System.Threading.Tasks;
using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;

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

        public UnitOfWork(AdviLawDBContext dbContext)
        {
            _dbContext = dbContext;

            GenericLawyers = new GenericRepository<Lawyer>(_dbContext);
            GenericClients = new GenericRepository<Client>(_dbContext);
            JobFields = new JobFieldRepository(_dbContext);
            Jobs = new JobRepository(_dbContext);
            Lawyers = new LawyerRepository(_dbContext);
            RefreshTokens = new RefreshTokenRepository(_dbContext);
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
    }
}
