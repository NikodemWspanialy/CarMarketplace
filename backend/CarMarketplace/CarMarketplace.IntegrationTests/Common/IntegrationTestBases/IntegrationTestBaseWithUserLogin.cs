using CarMarketplace.Application.Authorization.Commands.RegisterUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithUserLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    private Guid UserId { get; set; }
    private static string UserEmail => "user@test.com";

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var command = new RegisterUserRequest(UserEmail, "TestPassword123!", "Test", "User");
        UserId = await SendAsync(command);
        Authenticate(UserId, UserEmail);
    }
}
