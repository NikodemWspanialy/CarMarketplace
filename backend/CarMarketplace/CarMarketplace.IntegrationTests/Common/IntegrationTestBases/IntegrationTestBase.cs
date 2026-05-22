using System.Net.Http.Headers;
using CarMarketplace.Domain.Users;
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

    protected HttpClient Client { get; } = factory.CreateClient();

    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }

    protected async Task SendAsync(IRequest request)
    {
        using var scope = factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.Send(request);
    }

    protected CarMarketplaceDbContext CreateDbContext()
    {
        var scope = factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<CarMarketplaceDbContext>();
    }

    protected void Authenticate(Guid userId, string email, UserRole role = UserRole.User)
    {
        var token = JwtTokenGenerator.Generate(userId, email, role.ToString());
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected void AuthenticateAsAdmin(Guid userId, string email) =>
        Authenticate(userId, email, UserRole.Admin);

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
