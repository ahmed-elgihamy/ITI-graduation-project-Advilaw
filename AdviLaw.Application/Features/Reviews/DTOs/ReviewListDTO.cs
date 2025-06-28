using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;

namespace AdviLaw.Application.Features.Reviews.DTOs
{
    public class LawyerReviewListDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Rate { get; set; }
        public ReviewType Type { get; set; } = ReviewType.ClientToLawyer;
        public string OtherPersonId { get; set; }
        public string OtherPersonName { get; set; } = string.Empty;
        public string OtherPersonPhotoUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
