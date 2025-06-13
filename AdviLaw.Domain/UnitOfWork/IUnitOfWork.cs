using AdviLaw.Domain.Entites;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.Entities.UserSection;



namespace AdviLaw.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Generic repositories
        IGenericRepository<Specialization> GenericSpecializations { get; }
        IGenericRepository<Lawyer> GenericLawyers { get; }

        // Specific repositories
        ISpecializationRepository Specializations { get; }
        Task<int> SaveChangesAsync();
    }
}
