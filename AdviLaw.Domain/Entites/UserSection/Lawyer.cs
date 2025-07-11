using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.SubscriptionSection;

namespace AdviLaw.Domain.Entities.UserSection
{

        public class Lawyer
        {
            public int Id { get; set; }

            public string? UserId { get; set; }
            public User? User { get; set; }


            public string ProfileHeader { get; set; } = string.Empty;
            public string ProfileAbout { get; set; } = string.Empty;
            public string? Bio { get; set; } = string.Empty;
            public bool IsApproved { get; set; }

          
           public int Experience { get; set; }

            public decimal HourlyRate { get; set; }
            public string BarCardImagePath { get; set; } = string.Empty;
            public string NationalIDImagePath { get; set; } = string.Empty;
            public int BarAssociationCardNumber { get; set; }

            public int Points { get; set; } = 0;

            public List<LawyerJobField> Fields { get; set; } 
            public List<Job> Jobs { get; set; } = new();
            public List<Proposal> Proposals { get; set; } = new();
            public List<Session> Sessions { get; set; } = new();
            public List<UserSubscription> UserSubscriptions { get; set; } = new();
        }


    }

