using AdviLaw.Domain.Entites.ScheduleSection;
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
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        private readonly AdviLawDBContext _dbContext;

        public ScheduleRepository(AdviLawDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Schedule>> GetSchedulesByLawyerId(Guid lawyerUserId)

        {
            var lawyerUserIdStr = lawyerUserId.ToString();

            var lawyer = await _dbContext.Lawyers
                .FirstOrDefaultAsync(l => l.UserId == lawyerUserIdStr);

            if (lawyer == null)
                return new List<Schedule>();

           return await _dbContext.Schedules
                //.Include(s => s.Job)
                //.Where(s => s.Job.LawyerId == lawyer.Id)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesByIds(List<int> list)
        {
            if (list == null || !list.Any())
            {
                return new List<Schedule>();
            }
            return await _dbContext.Schedules
                .Where(s => list.Contains(s.Id))
                .ToListAsync();
        }


    }
}

