using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdviLaw.Domain.IGenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null
        );

        Task<T?> GetByIdIncludesAsync(
            int id,
            Expression<Func<T, bool>>? filter = null,
            List<Expression<Func<T, object>>>? includes = null
        );
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task RemoveRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
     
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();


        Task<T?> FindFirstAsync(
       Expression<Func<T, bool>> predicate,
       List<Expression<Func<T, object>>>? includes = null
   );

    }
}
