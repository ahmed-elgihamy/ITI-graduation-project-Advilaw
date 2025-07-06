using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Infrastructure.Repositories
{
    public class EscrowRepository(AdviLawDBContext dbContext) : GenericRepository<EscrowTransaction>(dbContext), IEscrowRepository
    {
        private readonly AdviLawDBContext _dbContext = dbContext;
    }
}
