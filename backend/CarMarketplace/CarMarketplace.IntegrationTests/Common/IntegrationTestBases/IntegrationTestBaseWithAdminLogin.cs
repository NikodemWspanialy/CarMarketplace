using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithAdminLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    private Guid AdminId { get; set; }
    private static string AdminEmail => "admin@test.com";

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var command = new RegisterUserRequest(AdminEmail, "TestPassword123!", "Admin", "User");
        AdminId = await SendAsync(command);

        await SendAsync(new UpgradeToAdminRequest(AdminId));
        Authenticate(AdminId, AdminEmail, UserRole.Admin);
    }
}
