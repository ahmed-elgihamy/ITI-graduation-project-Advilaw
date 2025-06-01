using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entities.UserSection
{
    public class Client
    {
        public int Id { get; set; }

        //                       FK to User                       //
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
