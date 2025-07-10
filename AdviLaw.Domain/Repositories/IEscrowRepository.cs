using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Repositories
{
    public interface IEscrowRepository : IGenericRepository<EscrowTransaction>
    {
    }
}
