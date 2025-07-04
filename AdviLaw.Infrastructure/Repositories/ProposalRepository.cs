using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Repositories;
using AdviLaw.Infrastructure.GenericRepo;
using AdviLaw.Infrastructure.Persistence;

namespace AdviLaw.Infrastructure.Repositories
{
    public class ProposalRepository(AdviLawDBContext dbContext) : GenericRepository<Proposal>(dbContext), IProposalRepository
    {
    }
}
