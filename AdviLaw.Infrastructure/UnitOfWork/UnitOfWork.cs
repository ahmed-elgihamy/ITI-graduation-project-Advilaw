using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Entites;
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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdviLawDBContext _dbContext;

        public ISpecializationRepository Specializations { get; }
        public IJobFieldRepository JobFields { get; }

        public IGenericRepository<Specialization> GenericSpecializations { get; }
        public IGenericRepository<Lawyer> GenericLawyers { get; }


        public UnitOfWork(AdviLawDBContext dbContext)
        {
            _dbContext = dbContext;
            Specializations = new SpecializationRepository(_dbContext);

            GenericSpecializations = new GenericRepository<Specialization>(_dbContext);
            GenericLawyers = new GenericRepository<Lawyer>(_dbContext);

            JobFields = new JobFieldRepository(_dbContext);


        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
