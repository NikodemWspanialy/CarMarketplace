using CarMarketplace.Application.Admin.Commands.DowngradeToUser;
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

public class DowngradeToUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task DowngradeToUser_WithAdminUser_ChangesRoleToUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new UpgradeToAdminRequest(userId));

        // Act
        await SendAsync(new DowngradeToUserRequest(userId));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user!.Role.Should().Be(UserRole.User);
    }

    [Fact]
    public async Task DowngradeToUser_WhenAlreadyUser_ThrowsDomainException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new DowngradeToUserRequest(userId));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
