using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
  public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesByLawyerId(int lawyerId);
    }
}
