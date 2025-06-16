using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace AdviLaw.Infrastructure.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AdviLawDBContext _dbContext;

        public GenericRepository(AdviLawDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(ICollection<T> entities) => await _dbContext.Set<T>().AddRangeAsync(entities);

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetTableNoTracking() => _dbContext.Set<T>().AsNoTracking();

        public IQueryable<T> GetTableAsTracking() => _dbContext.Set<T>();
        public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);

        }
    }
}
