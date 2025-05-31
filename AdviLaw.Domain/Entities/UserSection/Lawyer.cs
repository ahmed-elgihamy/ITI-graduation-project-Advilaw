using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdviLaw.Domain.Entities.UserSection
{
public class Lawyer
    {
        public int Id { get; set; }

        //                       FK to User                       //
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string ProfileHeader { get; set; } = string.Empty;
        public string ProfileAbout { get; set; } = string.Empty;
        public int LawyerCardID { get; set; }

    }
}
