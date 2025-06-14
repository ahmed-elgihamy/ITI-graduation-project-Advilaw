using AdviLaw.Domain.Entities.UserSection;

namespace AdviLaw.Domain.Entites.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public int NationalityId { get; set; }
        public string ImageUrl { get; set; }
        public Gender Gender { get; set; }

        public string UserType { get; set; } // Lawyer / Client / Admin

        // Lawyer Only:
        public string? ProfileHeader { get; set; }
        public string? ProfileAbout { get; set; }
        public int? LawyerCardID { get; set; }
        public string? BarCardImagePath { get; set; }
        public int? BarAssociationCardNumber { get; set; }
        public string? Bio { get; set; }
    }
}
