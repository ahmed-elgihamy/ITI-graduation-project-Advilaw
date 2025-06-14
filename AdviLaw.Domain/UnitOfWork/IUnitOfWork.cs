using System.Threading.Tasks;
using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;

namespace AdviLaw.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
       
        IGenericRepository<Specialization> GenericSpecializations { get; }
        IGenericRepository<Lawyer> GenericLawyers { get; }

   
        IJobFieldRepository JobFields { get; }
        ILawyerRepository Lawyers { get; }
        IJobRepository Jobs { get; }

        Task<int> SaveChangesAsync();
    }
}
