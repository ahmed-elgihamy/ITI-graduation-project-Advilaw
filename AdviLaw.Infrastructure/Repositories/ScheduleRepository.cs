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

        public async Task<List<Schedule>> GetSchedulesByLawyerId(int lawyerId)
        {
            return await _dbContext.Schedules
                .Include(s => s.Job)
                .Where(s => s.Job.LawyerId == lawyerId)
                .OrderByDescending(s => s.Id)
                .ToListAsync();
        }
    }
}

