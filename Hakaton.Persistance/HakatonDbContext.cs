using Microsoft.EntityFrameworkCore;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using Hakaton.Persistance.HakatonTypeConfigurations;


namespace Hakaton.Persistance
{
    public class HakatonDbContext : DbContext, IHakatonDbContext
    {
        public DbSet<User> Users { get; set; }
        public HakatonDbContext(DbContextOptions<HakatonDbContext> options) : base(options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HakatonConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
