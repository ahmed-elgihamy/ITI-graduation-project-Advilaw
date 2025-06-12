using AdviLaw.Domain.Entites;
using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationRepository
    {
        private readonly AdviLawDBContext _dbContext;

        public SpecializationRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}

