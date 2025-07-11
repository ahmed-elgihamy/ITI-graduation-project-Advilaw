using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.IGenericRepo;

namespace AdviLaw.Domain.Repositories
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesByLawyerId(Guid lawyerId);
        Task<List<Schedule>> GetSchedulesByIds(List<int> list);
    }

}
