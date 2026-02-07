using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Persistence;

internal class CarMarketplaceDbContext(DbContextOptions<CarMarketplaceDbContext> opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}