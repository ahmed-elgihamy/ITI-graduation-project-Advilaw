using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entities.UserSection;

using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
   public interface ILawyerRepository : IGenericRepository<Lawyer>
    {
        Task<(IEnumerable<Lawyer>, int)> GetAllMatchingAsync(string? searchPhrase, int PageSize, int PageNumber);

    }
}
