using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdviLaw.Domain.IGenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task SaveChangesAsync();
    }
}
