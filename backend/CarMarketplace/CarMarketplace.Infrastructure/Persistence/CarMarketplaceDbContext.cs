using CarMarketplace.Domain.Cars;
using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Persistence;

public class CarMarketplaceDbContext(DbContextOptions<CarMarketplaceDbContext> opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarMarketplaceDbContext).Assembly);
    }
}