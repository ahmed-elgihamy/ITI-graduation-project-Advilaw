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
        Task DeleteAsync(T entity);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
    }
}
