using AdviLaw.Domain.Entities.UserSection;
using AdviLaw.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.LawyerSection.DTOs
{
    public class LawyerListDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Client;
        public Gender Gender { get; set; }
        public bool IsApproved { get; set; }
        public string ProfileHeader { get; set; } = string.Empty;
    }
}