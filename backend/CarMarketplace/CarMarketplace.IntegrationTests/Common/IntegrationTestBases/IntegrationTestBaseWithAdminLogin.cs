using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithAdminLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    private Guid AdminId { get; set; }
    private string AdminEmail => Faker.Internet.Email();
    private string AdminPassword => Faker.Internet.Password();
    private string AdminFirstName => Faker.Name.FirstName();
    private string AdminLastName => Faker.Name.LastName();

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var command = new RegisterUserRequest(AdminEmail, AdminPassword, AdminFirstName, AdminLastName);
        AdminId = await SendAsync(command);

        await SendAsync(new UpgradeToAdminRequest(AdminId));
        await SendAsync(new LoginUserQuery(AdminEmail, AdminPassword));
    }
}
