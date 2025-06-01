using AdviLaw.Domain.Entities;
using AdviLaw.Domain.Entities.UserSection;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Persistence
{
    public class AdviLawDBContext : DbContext
    {
        public AdviLawDBContext(DbContextOptions<AdviLawDBContext> options) : base(options)
        {
        }

        public DbSet<Specialization> specializations { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //********* Composition**********//

            //**Lawyer**//
            modelBuilder.Entity<User>()
               .HasOne(u => u.Lawyer)
               .WithOne(l => l.User)
                .HasForeignKey<Lawyer>(l => l.UserId);
            //**Client**//
            modelBuilder.Entity<User>()
              .HasOne(u => u.Client)
              .WithOne(a => a.User)
              .HasForeignKey<Client>(a => a.UserId);
            //**Admin**//
            modelBuilder.Entity<User>()
              .HasOne(u => u.Admin)
              .WithOne(a => a.User)
              .HasForeignKey<Admin>(a => a.UserId);


        }
    }
}
