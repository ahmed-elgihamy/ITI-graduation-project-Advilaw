using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using MediatR;

namespace AdviLaw.Application.Features.ProposalSection.Query.GetProposalsByJobIdForLawyer
{
    public class GetProposalsByJobIdForLawyerQuery : IRequest<Response<ProposalDetails>>
    {
        public int ProposalId { get; set; }
        public int LawyerId { get; set; }
        public GetProposalsByJobIdForLawyerQuery(int proposalId, int lawyerId)
        {
            ProposalId = proposalId;
            LawyerId = lawyerId;
        }
    }
}
