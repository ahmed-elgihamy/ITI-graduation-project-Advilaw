using AdviLaw.Domain.Entites.ProposalSection;

namespace AdviLaw.Application.Features.ProposalSection.DTOs
{
    public class CreatedProposalDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Budget { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.None;
        public int JobId { get; set; }
    }
}
