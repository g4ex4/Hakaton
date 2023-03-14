using Hakaton.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hakaton.Application.Interfaces
{
    public interface IHakatonDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
