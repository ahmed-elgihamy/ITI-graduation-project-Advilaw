using AdviLaw.Domain.Entities.UserSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entites.RefreshToken
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
