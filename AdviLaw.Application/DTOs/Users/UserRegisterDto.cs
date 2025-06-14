using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Enums;

namespace AdviLaw.Application.DTOs.Users
{
    public class UserRegisterDto
    {
        // Shared (User)
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public Roles Role { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int NationalityId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;


        // Lawyer-specific
        public string? ProfileHeader { get; set; }
        public string? ProfileAbout { get; set; }
        public int? LawyerCardID { get; set; }
        public string? Bio { get; set; }
        public string? BarCardImagePath { get; set; }
        public int? BarAssociationCardNumber { get; set; }

        // Client-specific..
        //none

    }
}
