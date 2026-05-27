using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Admin.Commands.UnbanUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class UnbanUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task UnbanUser_WhenBanned_UnbansUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("unban@example.com", "Password123!", "Unban", "Me"));
        await SendAsync(new BanUserRequest(userId, "Violation"));

        // Act
        await SendAsync(new UnbanUserRequest(userId, "Appealed successfully"));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsBanned.Should().BeFalse();
    }
}
