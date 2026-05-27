using CarMarketplace.Application.Admin.Commands.DeleteUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class DeleteUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task DeleteUser_WithExistingUser_SoftDeletesUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("delete@example.com", "Password123!", "Delete", "Me"));

        // Act
        await SendAsync(new DeleteUserRequest(userId));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsDeleted.Should().BeTrue();
    }
}
