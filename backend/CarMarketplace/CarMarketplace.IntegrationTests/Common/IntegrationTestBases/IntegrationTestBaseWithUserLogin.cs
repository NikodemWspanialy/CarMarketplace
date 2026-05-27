using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;

namespace CarMarketplace.IntegrationTests.Common.IntegrationTestBases;

public abstract class IntegrationTestBaseWithUserLogin(CarMarketplaceApiFactory factory)
    : IntegrationTestBase(factory)
{
    protected Guid UserId { get; private set; }
    protected string UserEmail => Faker.Internet.Email();
    protected string UserPassword => Faker.Internet.Password();
    protected string UserFirstName => Faker.Name.FirstName();
    protected string UserLastName => Faker.Name.LastName();

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var registerCommand = new RegisterUserRequest(UserEmail, UserPassword, UserFirstName, UserLastName);
        UserId = await SendAsync(registerCommand);
        
        var loginCommand = new LoginUserQuery(UserEmail, UserPassword);
        await SendAsync(loginCommand);
    }
}
