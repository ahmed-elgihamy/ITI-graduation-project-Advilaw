using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.AdminSection.DTOs
{
    public class EditAdminProfileDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? PostalCode { get; set; }
        public long? NationalityId { get; set; }
        public string? ImageUrl { get; set; }
        public Gender Gender { get; set; }
    }
} 