using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Application.Features.AdminSection.DTOs
{
    public class LawyerForAdminDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string BarCardImagePath { get; set; } = string.Empty;
        public string NationalIDImagePath { get; set; } = string.Empty;
        public int BarAssociationCardNumber { get; set; }
        public long NationalityId { get; set; }
        public bool IsApproved { get; set; }
    }
}
