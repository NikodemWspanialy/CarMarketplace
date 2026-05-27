using CarMarketplace.Application.Authorization.Commands.RegisterUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithUserLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    protected Guid UserId { get; private set; }

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var command = new RegisterUserRequest("user@test.com", "TestPassword123!", "Test", "User");
        UserId = await SendAsync(command);
    }
}
