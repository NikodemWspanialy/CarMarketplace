using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Persistence;

public class CarMarketplaceDbContext(DbContextOptions<CarMarketplaceDbContext> opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }
}