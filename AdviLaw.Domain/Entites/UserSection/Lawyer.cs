using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
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
        public string? UserId { get; set; }
        public User? User { get; set; } 

        public string ProfileHeader { get; set; } = string.Empty;
        public string ProfileAbout { get; set; } = string.Empty;
        public int LawyerCardID { get; set; }

        public string Bio { get; set; } = string.Empty;
        public bool IsApproved { get; set; }
        public string barCardImagePath { get; set; } = string.Empty;
        public int barAssociationCardNumber { get; set; }

        //Fields
        public List<LawyerJobField> Fields { get; set; } = new();

        //Job Section
        public List<Job> Jobs { get; set; } = new();
        public List<Proposal> Proposals { get; set; } = new();


        //Session Section
        public List<Session> Sessions { get; set; } = new();

        //UserSubscription
        public List<UserSubscription> UserSubscriptions { get; set; } = new();


    }
}
