using AdviLaw.Domain.Entites;

namespace AdviLaw.Domain.Repositories;

public interface ISpecializationRepository
{
    Task<IEnumerable<Specialization>> GetAllAsync();
    Task<Specialization?> GetByIdAsync(int id);
    Task<int> Create(Specialization specialization);

}
