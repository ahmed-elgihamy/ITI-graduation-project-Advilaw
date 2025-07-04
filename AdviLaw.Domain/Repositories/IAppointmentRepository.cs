using AdviLaw.Domain.Entites.AppointmentSection;
using AdviLaw.Domain.IGenericRepo;

namespace AdviLaw.Domain.Repositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment> GetLastAppointmentAsync(int jobId);
    }
}
