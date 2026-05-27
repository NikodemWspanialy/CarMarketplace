using System.Net;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class UpgradeToAdminTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task UpgradeToAdmin_WithExistingUser_ReturnsNoContent()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("target@example.com", "Password123!", "Target", "User"));

        // Act
        var response = await Client.PutAsync($"/api/admin/upgrade-to-admin/{userId}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.Role.Should().Be(UserRole.Admin);
    }
}
