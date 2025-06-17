using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class JobRepository(AdviLawDBContext dbContext) : GenericRepository<Job>(dbContext), IJobRepository
    {
        public async Task<IQueryable<Job>> GetAllActivePublishedJobs()
        {
            var jobs = _dbContext.Jobs
                .Include(j => j.Client)
                    .ThenInclude(c => c.User)
                .Include(j => j.JobField)
                .Where(j => j.Type == JobType.ClientPublishing)
                .Where(j => j.Status == JobStatus.NotAssigned);
            return jobs;
        }
    }
}
