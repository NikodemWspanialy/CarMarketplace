using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithUserLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    protected Guid UserId { get; private set; }
    protected string UserEmail { get; private set; } = null!;
    protected string UserPassword { get; private set; } = null!;
    protected string UserFirstName { get; private set; } = null!;
    protected string UserLastName { get; private set; } = null!;

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        UserEmail = Faker.Internet.Email();
        UserPassword = Faker.Internet.Password();
        UserFirstName = Faker.Name.FirstName();
        UserLastName = Faker.Name.LastName();

        var registerCommand = new RegisterUserRequest(UserEmail, UserPassword, UserFirstName, UserLastName);
        UserId = await SendAsync(registerCommand);

        await SendAsync(new LoginUserQuery(UserEmail, UserPassword));
    }
}
