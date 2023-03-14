using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Hakaton.Application.Interfaces;

namespace Hakaton.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance (this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<HakatonDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IHakatonDbContext>(provider =>
            provider.GetService<HakatonDbContext>());
            return services;
        }
    }
}
