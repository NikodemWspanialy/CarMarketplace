using CarMarketplace.Application.Common.Interfaces;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

namespace CarMarketplace.IntegrationTests.Common;

public class CarMarketplaceApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithDatabase("car_tests")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public FakeCurrentUserProvider FakeCurrentUserProvider { get; } = new();

    public string ConnectionString => _dbContainer.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<CarMarketplaceDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            // Register DbContext with Testcontainers connection string
            services.AddDbContext<CarMarketplaceDbContext>(opt =>
                opt.UseNpgsql(_dbContainer.GetConnectionString()));

            // Replace ICurrentUserProvider with fake
            var currentUserDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(ICurrentUserProvider));

            if (currentUserDescriptor is not null)
                services.Remove(currentUserDescriptor);

            services.AddSingleton<ICurrentUserProvider>(FakeCurrentUserProvider);
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        // Apply migrations
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CarMarketplaceDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await base.DisposeAsync();
    }
}
