﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
    }
}
