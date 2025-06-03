using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class SpecializationRepository(AdviLawDBContext dbContext) : ISpecializationRepository
    {
        private readonly AdviLawDBContext _dbContext = dbContext;

        public async Task<int> Create(Specialization specialization)
        {
            _dbContext.Specializations.Add(specialization);
           await _dbContext.SaveChangesAsync();
            return specialization.Id;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            var specializations = await _dbContext.Specializations.ToListAsync();
            return specializations;
        }

        public async Task<Specialization?> GetByIdAsync(int id)
        {
            var specialization = await _dbContext.Specializations.FirstOrDefaultAsync(x => x.Id == id);
            return specialization;
        }
    }
}
