using AdviLaw.Domain.Repositories;
using AdviLaw.Domain.UnitOfWork;
using AdviLaw.Infrastructure.Persistence;
using AdviLaw.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdviLawDBContext _dbContext;

        public ISpecializationRepository Specializations { get; }
        public IJobFieldRepository JobFields { get; }

        public UnitOfWork(AdviLawDBContext dbContext)
        {
            _dbContext = dbContext;
            Specializations = new SpecializationRepository(_dbContext);
            JobFields = new JobFieldRepository(_dbContext);

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
