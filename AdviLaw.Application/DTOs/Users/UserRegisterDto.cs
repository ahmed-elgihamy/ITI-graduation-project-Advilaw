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
        public Roles Role { get; set; } 

        // Lawyer-specific
        public string? ProfileHeader { get; set; }
        public string? ProfileAbout { get; set; }
        public int? LawyerCardID { get; set; }
        public string? Bio { get; set; }
        public string? BarCardImagePath { get; set; }
        public int? BarAssociationCardNumber { get; set; }

        // Client-specific..
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
