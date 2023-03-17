using Microsoft.EntityFrameworkCore;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using Hakaton.Persistance.HakatonTypeConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Hakaton.Persistance
{
    public class HakatonDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public HakatonDbContext(DbContextOptions<HakatonDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
             builder.Entity<Note>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string? connectionString = config
                .GetConnectionString("ConnectionString");

            optionsBuilder
                .UseSqlServer(connectionString);
        }
    }
}
