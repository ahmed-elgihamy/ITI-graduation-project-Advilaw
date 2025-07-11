using AdviLaw.Domain.Entites;
using AdviLaw.Domain.Entites.JobSection;
using AdviLaw.Domain.Entites.PaymentSection;
using AdviLaw.Domain.Entites.SessionSection;
using AdviLaw.Domain.Entites.SubscriptionSection;
using AdviLaw.Domain.Entities.UserSection;
using Microsoft.EntityFrameworkCore;
using AdviLaw.Domain.Entites.EscrowTransactionSection;
using AdviLaw.Domain.Entites.ProposalSection;
using AdviLaw.Domain.Entites.ScheduleSection;
using AdviLaw.Domain.Entites.SessionUtilities.MessageSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReportSection;
using AdviLaw.Domain.Entites.SessionUtilities.ReviewSection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AdviLaw.Domain.Entites.RefreshToken;
using AdviLaw.Domain.Entites.Auth;
using AdviLaw.Domain.Entites.AppointmentSection;

namespace AdviLaw.Infrastructure.Persistence
{
    public class AdviLawDBContext : IdentityDbContext<User>
    {
        public AdviLawDBContext(DbContextOptions<AdviLawDBContext> options) : base(options) { }

        //public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EscrowTransaction> EscrowTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<JobField> JobFields { get; set; }
        public DbSet<LawyerJobField> LawyerJobFields { get; set; }
        public DbSet<PlatformSubscription> PlatformSubscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<SubscriptionPoint> SubscriptionPoints { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ********* Composition ********** //

            // Lawyer
            modelBuilder.Entity<User>()
                .HasOne(u => u.Lawyer)
                .WithOne(l => l.User)
                .HasForeignKey<Lawyer>(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Client
            modelBuilder.Entity<User>()
                .HasOne(u => u.Client)
                .WithOne(c => c.User)
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Admin
            modelBuilder.Entity<User>()
                .HasOne(u => u.Admin)
                .WithOne(a => a.User)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            // ******** Relations *********** //
            // ..............................User Relations............................
            //Reviews
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedReviews)
                .WithOne(r => r.Reviewee)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .HasMany(u => u.SentReviews)
                .WithOne(r => r.Reviewer)
                .OnDelete(DeleteBehavior.Restrict);



            //Reports
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedReports)
                .WithOne(r => r.Receiver)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .HasMany(u => u.SentReports)
                .WithOne(r => r.Sender)
                .OnDelete(DeleteBehavior.Restrict);



            //Messages
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.Restrict);


            //Payments
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedPayments)
                .WithOne(m => m.Receiver)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<User>()
                .HasMany(u => u.SentPayments)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.Restrict);


            //Escrow
            modelBuilder.Entity<User>()
                .HasMany(u => u.EscrowTransactions)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.Restrict);



            // ..............................Lawyer Relations............................


            //Jobs
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Jobs)
                .WithOne(j => j.Lawyer)
                .OnDelete(DeleteBehavior.Restrict);

            //Proposals
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Proposals)
                .WithOne(p => p.Lawyer)
                .OnDelete(DeleteBehavior.Restrict);


            //Sessions
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Sessions)
                .WithOne(s => s.Lawyer)
                .OnDelete(DeleteBehavior.Restrict);


            //JobField
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Fields)
                .WithOne(f => f.Lawyer)
                .OnDelete(DeleteBehavior.Restrict);

            //Subscription
            modelBuilder.Entity<Lawyer>()
                .HasMany(u => u.UserSubscriptions)
                .WithOne(m => m.Lawyer)
                .OnDelete(DeleteBehavior.Restrict);



            // ..............................Client Relations............................

            //Jobs
            modelBuilder.Entity<Client>()
                .HasMany(l => l.Jobs)
                .WithOne(j => j.Client)
                .OnDelete(DeleteBehavior.Restrict);


            //Sessions
            modelBuilder.Entity<Client>()
                .HasMany(l => l.Sessions)
                .WithOne(s => s.Client)
                .OnDelete(DeleteBehavior.Restrict);


            // ..............................Session Relations............................

            //One To One
            //Jobs
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Job)
                .WithOne(j => j.Session)
                .OnDelete(DeleteBehavior.Restrict);


            //EscrowTransactions
            modelBuilder.Entity<Session>()
            .HasOne(s => s.EscrowTransaction)
             .WithOne(e => e.Session)
            .HasForeignKey<EscrowTransaction>(e => e.SessionId)  //  FK
            .OnDelete(DeleteBehavior.Restrict);


            //Payments
            modelBuilder.Entity<Session>()
            .HasOne(s => s.Payment)
            .WithOne(p => p.Session)
            .HasForeignKey<Payment>(p => p.SessionId)
            .OnDelete(DeleteBehavior.Restrict);


            //One To Many
            //Messages
            modelBuilder.Entity<Message>()
    .HasOne(m => m.Session)
    .WithMany(s => s.Messages)
    .HasForeignKey(m => m.SessionId)
    .OnDelete(DeleteBehavior.Restrict);


            //Reviews
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Reviews)
                .WithOne(m => m.Session)
                .OnDelete(DeleteBehavior.Restrict);


            //Reports
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Reports)
                .WithOne(m => m.Session)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
     .HasOne(r => r.Reviewer)
     .WithMany()
     .HasForeignKey(r => r.ReviewerId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewee)
                .WithMany()
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);




            // ..............................Job Relations............................

            //One to one
            //Proposal
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Proposals)
                .WithOne(p => p.Job)
                .OnDelete(DeleteBehavior.Restrict);


            //Escrow
            modelBuilder.Entity<Job>()
           .HasOne(j => j.EscrowTransaction)
           .WithOne(et => et.Job)
           .HasForeignKey<EscrowTransaction>(et => et.JobId)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Job>()
             .HasOne(j => j.Session)
             .WithOne(s => s.Job)
             .HasForeignKey<Session>(s => s.JobId)
              .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Job>()
            //    .HasMany(j => j.Schedules)
            //    .WithOne(s => s.Job)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Appointments)
                .HasForeignKey(j => j.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            //One to Many
            //JobFields
            modelBuilder.Entity<Job>()
                .HasOne(j => j.JobField)
                .WithMany(l => l.Jobs)
                .OnDelete(DeleteBehavior.Restrict);


            // ..............................Payment Relations............................
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.EscrowTransaction)
                .WithOne(e => e.Payment)
                .HasForeignKey<EscrowTransaction>(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscription>()
             .HasOne(us => us.Payment)
              .WithOne(p => p.UserSubscription)
              .HasForeignKey<UserSubscription>(us => us.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);


            // ..............................JobField Relations............................
            modelBuilder.Entity<JobField>()
                .HasMany(j => j.LawyerJobs)
                .WithOne(lj => lj.JobField)
                .OnDelete(DeleteBehavior.Restrict);


            // ..............................Platform Subscription Relations............................
            modelBuilder.Entity<PlatformSubscription>()
                .HasMany(ps => ps.UsersSubscriptions)
                .WithOne(us => us.SubscriptionType)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PlatformSubscription>()
                .HasMany(ps => ps.Details)
                .WithOne(d => d.PlatformSubscription)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<LawyerJobField>()
            .HasKey(lj => new { lj.LawyerId, lj.JobFieldId });


            modelBuilder.Entity<UserSubscription>()
           .HasOne(us => us.Lawyer)
            .WithMany(l => l.UserSubscriptions)
            .HasForeignKey(us => us.LawyerId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.SubscriptionType)
                .WithMany(ps => ps.UsersSubscriptions)
                .HasForeignKey(us => us.SubscriptionTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Payment)
                .WithOne(p => p.UserSubscription)
                .HasForeignKey<UserSubscription>(us => us.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<EscrowTransaction>()
              .HasOne(e => e.Payment)
              .WithOne(p => p.EscrowTransaction)
             .HasForeignKey<EscrowTransaction>(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);



            //To store them as strings more readable in DB
            modelBuilder.Entity<User>()
              .Property(u => u.Role)
              .HasConversion<string>();

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.NationalityId)
                      .HasColumnType("bigint"); // Forces SQL Server to use BIGINT
            });

        }
    }
}