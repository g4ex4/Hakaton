using Microsoft.EntityFrameworkCore;
using Hakaton.Application.Interfaces;
using Hakaton.Domain;
using Hakaton.Persistance.HakatonTypeConfigurations;
using Microsoft.Extensions.Configuration;


namespace Hakaton.Persistance
{
    public class HakatonDbContext : DbContext, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public HakatonDbContext(DbContextOptions<HakatonDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
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
