using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdviLaw.Infrastructure.Repositories
{
    public class LawyerRepository : GenericRepository<Lawyer>, ILawyerRepository
    {
        private readonly AdviLawDBContext dBContext;

        public LawyerRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
            this.dBContext = dbContext;
        }

        public async Task<(IEnumerable<Lawyer>, int)> GetAllMatchingAsync(string? searchPhrase, int PageSize, int PageNumber)
        {
            var searchToLower = string.IsNullOrWhiteSpace(searchPhrase) ? null : searchPhrase.Trim().ToLower();

            var baseQuery = dBContext.Lawyers
       .Include(l => l.User)
       .Include(l => l.Fields).ThenInclude(f => f.JobField)
       .Where(C => searchToLower == null || C.User.UserName.ToLower().Contains(searchToLower));



            var TotalCount = await baseQuery.CountAsync();

            //if (SortBy != null)
            //{

            //    var ColumnSelector = new Dictionary<string, Expression<Func<Lawyer, object>>>
            //{
            //    {nameof(Lawyer.User.UserName),r=>r.User.UserName },

            //};
            //    var SelectedColumn = ColumnSelector[SortBy];

            //    baseQuery = sortDirection == SortDirection.Ascending
            //        ? baseQuery.OrderBy(SelectedColumn)
            //        : baseQuery.OrderByDescending(SelectedColumn);

            //}

            var lawyers = await baseQuery
                 .Skip(PageSize * (PageNumber - 1))
                 .Take(PageSize)
                .ToListAsync();


            return (lawyers, TotalCount);
        }
    }
}
