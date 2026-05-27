using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Exceptions;
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
    public async Task UpgradeToAdmin_WithExistingUser_ChangesRoleToAdmin()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new UpgradeToAdminRequest(userId));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.Role.Should().Be(UserRole.Admin);
    }

    [Fact]
    public async Task UpgradeToAdmin_WhenAlreadyAdmin_ThrowsDomainException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new UpgradeToAdminRequest(userId));

        // Act
        var act = () => SendAsync(new UpgradeToAdminRequest(userId));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
