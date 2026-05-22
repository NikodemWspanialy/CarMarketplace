using System.Net.Http.Headers;
using CarMarketplace.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Xunit;

namespace CarMarketplace.IntegrationTests.Common;

public abstract class IntegrationTestBase : IClassFixture<CarMarketplaceApiFactory>, IAsyncLifetime
{
    private readonly CarMarketplaceApiFactory _factory;
    private Respawner _respawner = null!;

    protected HttpClient Client { get; }

    protected IntegrationTestBase(CarMarketplaceApiFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient();
    }

    protected CarMarketplaceDbContext CreateDbContext()
    {
        var scope = _factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<CarMarketplaceDbContext>();
    }

    protected void Authenticate(Guid userId, string email, string role = "User")
    {
        var token = JwtTokenGenerator.Generate(userId, email, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected void AuthenticateAsAdmin(Guid userId, string email) =>
        Authenticate(userId, email, "Admin");

    protected void ClearAuthentication() =>
        Client.DefaultRequestHeaders.Authorization = null;

    public async Task InitializeAsync()
    {
        await using var connection = new NpgsqlConnection(_factory.ConnectionString);
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
