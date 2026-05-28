using CarMarketplace.Application.Admin.Commands.DeleteUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Exceptions;
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
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new DeleteUserRequest(userId));

        // Assert
        var user = await TestData.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Id == userId);
        user.Should().NotBeNull();
        user!.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteUser_WhenAlreadyDeleted_ThrowsDomainException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new DeleteUserRequest(userId));

        // Act
        var act = () => SendAsync(new DeleteUserRequest(userId));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
