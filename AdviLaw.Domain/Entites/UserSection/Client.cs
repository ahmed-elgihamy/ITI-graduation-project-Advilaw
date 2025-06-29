using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.SessionSection;
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
        public bool IsApproved { get; set; }


        //                       FK to User                       //
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string NationalIDImagePath { get; set; } = string.Empty;


        //Job Section
        public List<Job> Jobs { get; set; } = new();

        //Session Section
        public List<Session> Sessions { get; set; } = new();


    }
}
