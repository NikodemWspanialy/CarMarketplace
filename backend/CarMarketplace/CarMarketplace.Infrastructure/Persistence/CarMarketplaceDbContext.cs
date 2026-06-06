using CarMarketplace.Domain.Cars;
using CarMarketplace.Domain.ContactReveals;
using CarMarketplace.Domain.Contacts;
using CarMarketplace.Domain.Listings;
using CarMarketplace.Domain.ListingViews;
using CarMarketplace.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CarMarketplace.Infrastructure.Persistence;

public class CarMarketplaceDbContext(DbContextOptions<CarMarketplaceDbContext> opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Car> Cars { get; set; }

    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Listing> Listings { get; set; }

    public DbSet<ListingView> ListingViews { get; set; }

    public DbSet<ContactReveal> ContactReveals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarMarketplaceDbContext).Assembly);
    }
}