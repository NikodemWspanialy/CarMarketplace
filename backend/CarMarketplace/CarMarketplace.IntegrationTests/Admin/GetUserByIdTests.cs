using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Users.Queries.GetUserById;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class GetUserByIdTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task GetUserById_WithExistingUser_ReturnsUserResponse()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("target@example.com", "Password123!", "John", "Doe"));

        // Act
        var result = await SendAsync(new GetUserByIdRequest(userId));

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("John");
    }
}
