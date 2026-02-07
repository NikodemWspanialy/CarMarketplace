using CarMarketplace.Application.Users.Helpers;
using CarMarketplace.Application.Users.Repositories;
using CarMarketplace.Infrastructure.Exceptions;
using CarMarketplace.Infrastructure.Persistence;
using CarMarketplace.Infrastructure.Repositories;
using CarMarketplace.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarMarketplace.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarMarketplaceDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection") ?? throw new NullConnectionString());
        });
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Others
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
    }
}