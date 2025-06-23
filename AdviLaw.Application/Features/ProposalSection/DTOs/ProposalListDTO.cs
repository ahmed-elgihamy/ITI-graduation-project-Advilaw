namespace AdviLaw.Application.Features.ProposalSection.DTOs
{
    public class ProposalListDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Budget { get; set; }

        //Navigation Properties
        public int LawyerId { get; set; }
        public string LawyerName { get; set; } = string.Empty; // Mapped from Lawyer.User.Name
    }
}
