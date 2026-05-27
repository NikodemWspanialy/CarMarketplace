using CarMarketplace.Application.Admin.Queries.GetUsers;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class GetUsersTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task GetUsers_WithExistingUsers_ReturnsPagedList()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("user1@example.com", "Password123!", "User", "One"));
        await SendAsync(new RegisterUserRequest("user2@example.com", "Password123!", "User", "Two"));

        // Act
        var result = await SendAsync(new GetUsersRequest(1, 10));

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}
