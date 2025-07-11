using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.LawyerSection.DTOs
{
    public class LawyerDetailsDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ProfileHeader { get; set; } = string.Empty;
        public string ProfileAbout { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public long NationalityId { get; set; }
        public Gender Gender { get; set; }
    }
}
