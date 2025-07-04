
using AdviLaw.Application.Basics;
using AdviLaw.Application.Features.ProposalSection.DTOs;
using AdviLaw.Domain.Entites.ProposalSection;
using MediatR;

namespace AdviLaw.Application.Features.ProposalSection.Command
{
    public class CreateProposalCommand : IRequest<Response<CreatedProposalDTO>>
    {
        public string Content { get; set; } = string.Empty;
        public int Budget { get; set; }
        public int JobId { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.None;
        public int LawyerId { get; set; }
        public CreateProposalCommand(string content, int budget, int jobId, int lawyerId) {
            Content = content;
            Budget = budget;
            JobId = jobId;
            LawyerId = lawyerId;
        }
    }
}
