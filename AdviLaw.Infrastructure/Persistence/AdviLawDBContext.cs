using AdviLaw.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdviLaw.Infrastructure.Persistence
{
    public class AdviLawDBContext : DbContext
    {
        public AdviLawDBContext(DbContextOptions<AdviLawDBContext> options) : base(options)
        {
        }

        public DbSet<Specialization> specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
