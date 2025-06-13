using AdviLaw.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISpecializationRepository Specializations { get; }
        IJobFieldRepository JobFields { get; }
        Task<int> SaveChangesAsync();
    }
}
