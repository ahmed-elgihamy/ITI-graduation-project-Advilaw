using AdviLaw.Domain.Entites.AppointmentSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Repositories
{
    public class AppointmentRepository(AdviLawDBContext dbContext) : GenericRepository<Appointment>(dbContext), IAppointmentRepository
    {
        AdviLawDBContext _dbContext = dbContext;
        public async Task<Appointment> GetLastAppointmentAsync(int jobId)
        {
            return await _dbContext.Appointments
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync();
        }
    }
}
