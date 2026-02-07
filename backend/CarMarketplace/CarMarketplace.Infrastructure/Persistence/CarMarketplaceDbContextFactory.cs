using CarMarketplace.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CarMarketplace.Infrastructure.Persistence;

public class CarMarketplaceDbContextFactory : IDesignTimeDbContextFactory<CarMarketplaceDbContext>
{
    public CarMarketplaceDbContext CreateDbContext(string[] args)
    {
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CarMarketplace.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CarMarketplaceDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new NullConnectionString();

        optionsBuilder.UseNpgsql(connectionString);

        return new CarMarketplaceDbContext(optionsBuilder.Options);
    }
}