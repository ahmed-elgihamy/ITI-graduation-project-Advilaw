using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class JobFieldRepository : GenericRepository<JobField>, IJobFieldRepository
    {
        public JobFieldRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
        }
    }
}
