using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.Features.Clients.DTOs
{
    public class ClientProfileDTO
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public string? UserId { get; set; }
        public string NationalIDImagePath { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;

        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public long NationalityId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public Gender Gender { get; set; }

        public string? StripeAccountId { get; set; }
        public decimal? Balance { get; set; } = 0; // بدل الـ Account
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;

    }
}
