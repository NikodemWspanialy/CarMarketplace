using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Admin.Commands.UnbanUser;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Exceptions;
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
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));

        // Act
        await SendAsync(new UnbanUserRequest(userId, Faker.Lorem.Sentence()));

        // Assert
        var user = await TestData.Users
            .Include(u => u.ActiveBan)
            .FirstOrDefaultAsync(u => u.Id == userId);
        user!.IsBanned.Should().BeFalse();
        user.ActiveBan.Should().BeNull();
    }

    [Fact]
    public async Task UnbanUser_WhenNotBanned_ThrowsDomainException()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new UnbanUserRequest(userId, Faker.Lorem.Sentence()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
