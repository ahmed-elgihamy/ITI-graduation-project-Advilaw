using AdviLaw.Domain.Entites;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.Entities.UserSection;



namespace AdviLaw.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Generic repositories
      
        IGenericRepository<Lawyer> GenericLawyers { get; }
        IGenericRepository<Client> GenericClients { get; }

        // Specific repositories
     
        IJobFieldRepository JobFields { get; }
      
        Task<int> SaveChangesAsync();
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
