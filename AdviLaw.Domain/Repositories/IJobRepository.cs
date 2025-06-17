using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IQueryable<Job>> GetAllActivePublishedJobs();
        Task<Job?> GetJobByIdForClient(int jobId);
        Task<Job?> GetJobByIdForLawyer(int jobId);
        Task<int> GetJobProposalCount(int jobId);
    }
}
