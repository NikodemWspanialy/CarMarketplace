using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class BanUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task BanUser_WithValidData_BansUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("ban@example.com", "Password123!", "Ban", "Me"));

        // Act
        await SendAsync(new BanUserRequest(userId, "Violation of terms"));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsBanned.Should().BeTrue();
    }
}
