using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CarMarketplace.Infrastructure.Persistence;

public class CarMarketplaceDbContextFactory : IDesignTimeDbContextFactory<CarMarketplaceDbContext>
{
    public CarMarketplaceDbContext CreateDbContext(string[] args)
    {
        var projectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "CarMarketplace.API");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CarMarketplaceDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        optionsBuilder.UseNpgsql(connectionString);

        return new CarMarketplaceDbContext(optionsBuilder.Options);
    }
}