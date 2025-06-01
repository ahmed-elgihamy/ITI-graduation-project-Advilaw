using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entities.UserSection
{
   public  class User : IdentityUser<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsConfirmedEmail { get; set; }
        public string Password { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsConfirmedPhone { get; set; }
        public int NationalityId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Gender Gender { get; set; }
        public decimal? Balance { get; set; } = 0; // بدل الAccount
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;


        //Navigation Properties




        //            Navigation to role info (composition)                //
        public Lawyer? Lawyer { get; set; }
        public Client? Client { get; set; }
        public Admin? Admin { get; set; }

    }
}
