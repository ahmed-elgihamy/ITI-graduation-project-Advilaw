using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdviLaw.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AdviLaw.Application.DTOs.Users
{
    public class UserRegisterDto
    {
        // Shared (User)
        // User Info
       
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Roles Role { get; set; } = Roles.Lawyer;
        public string City { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Country { get; set; } = string.Empty;
        public string? CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public long NationalityId { get; set; }

        public List<int>? FieldIds { get; set; }

        public IFormFile NationalIDImage { get; set; } = null!;
        public IFormFile? BarCardImage { get; set; } = null!;

        // 🧑‍⚖️ Lawyer Info
        public int? BarAssociationCardNumber { get; set; }

        // Lawyer-specific
        //public string? ProfileHeader { get; set; }
        //public string? ProfileAbout { get; set; }
        //public int? LawyerCardID { get; set; }
        //public string? Bio { get; set; }
        //public string? BarCardImagePath { get; set; }
        //public int? BarAssociationCardNumber { get; set; }

        // Client-specific..
        //none

    }
}
