using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;

namespace AdviLaw.Infrastructure.Repositories
{
    public class PaymentRepository(AdviLawDBContext dbContext) : GenericRepository<Payment>(dbContext), IPaymentRepository
    {
    }
}
