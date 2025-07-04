using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.ProposalSection.Query.GetProposalsByJobIdForClient
{
    public class GetProposalsByJobIdForClientQuery : IRequest<Response<ProposalDetails>>
    {
        public int ProposalId { get; set; }
        public int ClientId { get; set; }
        public GetProposalsByJobIdForClientQuery(int proposalId, int clientId)
        {
            ProposalId = proposalId;
            ClientId = clientId;
        }
    }
}
