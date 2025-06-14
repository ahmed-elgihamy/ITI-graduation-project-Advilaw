using System.Threading.Tasks;
using AdviLaw.Domain.Entites;
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

        // Specialized repositories  
        IJobFieldRepository JobFields { get; }
        ILawyerRepository Lawyers { get; }
        IJobRepository Jobs { get; }

        Task<int> SaveChangesAsync();
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
