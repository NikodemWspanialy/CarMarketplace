using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class LoginUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsAuthResponse()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("login@example.com", "Password123!", "Jane", "Doe"));
        var query = new LoginUserQuery("login@example.com", "Password123!");

        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
    }
}
