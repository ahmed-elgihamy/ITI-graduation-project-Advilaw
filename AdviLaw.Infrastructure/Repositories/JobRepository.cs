using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Repositories
{
    public class JobRepository(AdviLawDBContext dbContext) : GenericRepository<Job>(dbContext), IJobRepository
    {
        private readonly AdviLawDBContext _dbContext = dbContext;
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
        public async Task<Job?> GetJobByIdForClient(int jobId)
        {
            var job = await _dbContext.Jobs
                .Include(j => j.Client)
                    .ThenInclude(c => c.User)
                .Include(j => j.Lawyer)
                    .ThenInclude(l => l.User)
                .Include(j => j.JobField)
                .Include(j => j.Proposals)
                    .ThenInclude(p => p.Lawyer)
                        .ThenInclude(l => l.User)
                .Include(j => j.Appointments)
                .FirstOrDefaultAsync(j => j.Id == jobId);
            return job;
        }

        public async Task<Job?> GetJobByIdForLawyer(int jobId)
        {
            var job = await _dbContext.Jobs
                .Include(j => j.Client)
                    .ThenInclude(c => c.User)
                .Include(j => j.JobField)
                .Include(j => j.Appointments)
                .FirstOrDefaultAsync(j => j.Id == jobId);
            return job;
        }
        public async Task<int> GetJobProposalCount(int jobId)
        {
            var count = await _dbContext.Proposals
                .Where(p => p.JobId == jobId)
                .CountAsync();
            return count;
        }
    }
}
