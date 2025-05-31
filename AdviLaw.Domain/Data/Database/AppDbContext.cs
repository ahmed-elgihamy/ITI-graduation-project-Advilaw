using Microsoft.EntityFrameworkCore;
using server.Data.Entites.EscrowTransactionSection;
using server.Data.Entites.JobSection;
using server.Data.Entites.PaymentSection;
using server.Data.Entites.ProposalSection;
using server.Data.Entites.ScheduleSection;
using server.Data.Entites.SessionSection;
using server.Data.Entites.SessionUtilities.MessageSection;
using server.Data.Entites.SessionUtilities.ReportSection;
using server.Data.Entites.SessionUtilities.ReviewSection;
using server.Data.Entites.SubscriptionSection;
using server.Data.Entites.UserSection;
using server.Data.Tokens.RefreshTokenSection;

namespace server.Data.Database
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
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
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserRole")
                .HasValue<Admin>("Admin")
                .HasValue<Lawyer>("Lawyer")
                .HasValue<Client>("Client");

            // ******** Relations ***********
            // ..............................User Relations............................
            //Reviews
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedReviews)
                .WithOne(r => r.Reviewee);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentReviews)
                .WithOne(r => r.Reviewer);


            //Reports
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedReports)
                .WithOne(r => r.Receiver);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentReports)
                .WithOne(r => r.Sender);


            //Messages
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender);

            //Payments
            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedPayments)
                .WithOne(m => m.Receiver);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentPayments)
                .WithOne(m => m.Sender);

            //Escrow
            modelBuilder.Entity<User>()
                .HasMany(u => u.EscrowTransactions)
                .WithOne(m => m.Sender);

            modelBuilder.Entity<User>()
                .HasMany(u => u.RefreshTokens)
                .WithOne(r => r.User);

            // ..............................Lawyer Relations............................
            //Lawyer Fields
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Fields)
                .WithOne(f => f.Lawyer);

            //Jobs
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Jobs)
                .WithOne(j => j.Lawyer);

            //Proposals
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Proposals)
                .WithOne(p => p.Lawyer);

            //Sessions
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Sessions)
                .WithOne(s => s.Lawyer);

            //JobField
            modelBuilder.Entity<Lawyer>()
                .HasMany(l => l.Fields)
                .WithOne(f => f.Lawyer);

            //Subscription
            modelBuilder.Entity<Lawyer>()
                .HasMany(u => u.UserSubscriptions)
                .WithOne(m => m.Lawyer);


            // ..............................Client Relations............................

            //Jobs
            modelBuilder.Entity<Client>()
                .HasMany(l => l.Jobs)
                .WithOne(j => j.Client);

            //Sessions
            modelBuilder.Entity<Client>()
                .HasMany(l => l.Sessions)
                .WithOne(s => s.Client);

            // ..............................Session Relations............................

            //One To One
            //Jobs
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Job)
                .WithOne(j => j.Session);

            //EscrowTransactions
            modelBuilder.Entity<Session>()
                .HasOne(s => s.EscrowTransaction)
                .WithOne(e => e.Session);

            //Payments
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Payment)
                .WithOne(p => p.Session);

            //One To Many
            //Messages
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Messages)
                .WithOne(m => m.Session);

            //Reviews
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Reviews)
                .WithOne(m => m.Session);

            //Reports
            modelBuilder.Entity<Session>()
                .HasMany(s => s.Reports)
                .WithOne(m => m.Session);

            // ..............................Job Relations............................

            //One to one
            //Proposal
            modelBuilder.Entity<Job>()
                .HasMany(j => j.Proposals)
                .WithOne(p => p.Job);

            //Escrow
            modelBuilder.Entity<Job>()
                .HasOne(j => j.EscrowTransaction)
                .WithOne(e => e.Job);

            //One to Many
            //JobFields
            modelBuilder.Entity<Job>()
                .HasOne(j => j.JobField)
                .WithMany(l => l.Jobs);

            // ..............................Payment Relations............................

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.EscrowTransaction)
                .WithOne(e => e.Payment);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.UserSubscription)
                .WithOne(us => us.Payment);

            // ..............................JobField Relations............................
            modelBuilder.Entity<JobField>()
                .HasMany(j => j.LawyerJobs)
                .WithOne(lj => lj.JobField);

            // ..............................Platform Subscription Relations............................
            modelBuilder.Entity<PlatformSubscription>()
                .HasMany(ps => ps.UsersSubscriptions)
                .WithOne(us => us.SubscriptionType);

            modelBuilder.Entity<PlatformSubscription>()
                .HasMany(ps => ps.Details)
                .WithOne(d => d.PlatformSubscription);
        }
    }
}
