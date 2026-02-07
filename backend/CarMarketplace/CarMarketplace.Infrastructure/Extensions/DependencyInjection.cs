using CarMarketplace.Infrastructure.Exceptions;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarMarketplace.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        if (connectionString == null)
        {
            throw new NullConnectionString();
        }

        services.AddDbContext<CarMarketplaceDbContext>(opt => { opt.UseNpgsql(connectionString); });
    }
}