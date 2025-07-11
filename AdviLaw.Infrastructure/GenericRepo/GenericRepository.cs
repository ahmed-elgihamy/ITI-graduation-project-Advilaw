using AdviLaw.Domain.IGenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<T?> GetByIdIncludesAsync(
            int id,
            Expression<Func<T, bool>>? filter = null,
            List<Expression<Func<T, object>>>? includes = null
        )
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(e => EF.Property<int>(e, "Id") == id);

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null
        )
        {
            IQueryable<T> query = _dbContext.Set<T>();

            // Apply includes
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply filter
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply ordering
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }


        public async Task<T?> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(ICollection<T> entities) => await _dbContext.Set<T>().AddRangeAsync(entities);

        public async Task RemoveRangeAsync(ICollection<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }
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
        public async Task<T?> FindFirstAsync(
             Expression<Func<T, bool>> predicate,
             List<Expression<Func<T, object>>>? includes = null
         )
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

    }
}
