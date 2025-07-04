using AdviLaw.Application.Basics;
using MediatR;

namespace AdviLaw.Application.Features.ProposalSection.Command.AcceptProposal
{
    public class AcceptProposalCommand(int proposalId, int clientId) : IRequest<Response<bool>>
    {
        public int ProposalId { get; set; } = proposalId;
        public int ClientId { get; set; } = clientId;
    }
}
