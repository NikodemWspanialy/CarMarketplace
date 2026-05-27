using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithAdminLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    protected Guid AdminId { get; private set; }
    protected string AdminEmail { get; private set; } = null!;
    protected string AdminPassword { get; private set; } = null!;
    protected string AdminFirstName { get; private set; } = null!;
    protected string AdminLastName { get; private set; } = null!;

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        AdminEmail = Faker.Internet.Email();
        AdminPassword = Faker.Internet.Password();
        AdminFirstName = Faker.Name.FirstName();
        AdminLastName = Faker.Name.LastName();

        var command = new RegisterUserRequest(AdminEmail, AdminPassword, AdminFirstName, AdminLastName);
        AdminId = await SendAsync(command);

        await SendAsync(new UpgradeToAdminRequest(AdminId));
        await SendAsync(new LoginUserQuery(AdminEmail, AdminPassword));
    }
}
