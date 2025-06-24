using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}
