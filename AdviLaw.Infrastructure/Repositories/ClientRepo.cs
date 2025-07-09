using AdviLaw.Domain.Entities.UserSection;
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
    public class ClientRepo : GenericRepository<Client>, IClient
    {
        private readonly AdviLawDBContext _dbContext;

        public ClientRepo(AdviLawDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
