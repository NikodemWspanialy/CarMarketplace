using Bogus;
using CarMarketplace.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Xunit;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBase(CarMarketplaceApiFactory factory) : IClassFixture<CarMarketplaceApiFactory>, IAsyncLifetime
{
    private Respawner _respawner = null!;

    protected Faker Faker { get; } = new();

    protected CarMarketplaceDbContext TestData
    {
        get
        {
            var scope = factory.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<CarMarketplaceDbContext>();
        }
    }

    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }

    public async Task InitializeAsync()
    {
        await using var connection = new NpgsqlConnection(factory.ConnectionString);
        await connection.OpenAsync();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });

        await _respawner.ResetAsync(connection);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
