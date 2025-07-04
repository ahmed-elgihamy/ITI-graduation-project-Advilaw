using AdviLaw.Domain.Entites.ProposalSection;

namespace AdviLaw.Application.Features.ProposalSection.DTOs
{
    public class ProposalDetails
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Budget { get; set; }
        public ProposalStatus Status { get; set; }

        // Navigation Properties
        public int JobId { get; set; }
        public string JobHeader { get; set; } = string.Empty; //mapped
        public string JobDescription { get; set; } = string.Empty; //mapped
        public int JobBudget { get; set; } //mapped
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty; //mapped
    }
}
