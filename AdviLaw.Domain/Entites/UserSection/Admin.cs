using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entities.UserSection
{
   public class Admin
    {

        public int Id { get; set; }

        //                       FK to User                       //
        public string? UserId { get; set; } = string.Empty;
        public User? User { get; set; } = null!;

    }
}
