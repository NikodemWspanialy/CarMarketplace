using CarMarketplace.Application.Admin.Commands.DowngradeToUser;
using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class DowngradeToUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task DowngradeToUser_WithAdminUser_ChangesRoleToUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("promoted@example.com", "Password123!", "Promoted", "User"));
        await SendAsync(new UpgradeToAdminRequest(userId));

        // Act
        await SendAsync(new DowngradeToUserRequest(userId));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.Role.Should().Be(UserRole.User);
    }
}
