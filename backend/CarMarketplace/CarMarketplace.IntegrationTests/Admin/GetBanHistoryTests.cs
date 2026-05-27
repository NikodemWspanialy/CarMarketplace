using CarMarketplace.Application.Admin.Commands.BanUser;
using CarMarketplace.Application.Admin.Commands.UnbanUser;
using CarMarketplace.Application.Admin.Queries.GetBanHistory;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class GetBanHistoryTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task GetBanHistory_WithBannedUser_ReturnsBanRecords()
    {
        // Arrange
        var reason = Faker.Lorem.Sentence();
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new BanUserRequest(userId, reason));

        // Act
        var result = await SendAsync(new GetBanHistoryRequest(userId));

        // Assert
        result.Should().HaveCount(1);
        result[0].Reason.Should().Be(reason);
        result[0].UserId.Should().Be(userId);
        result[0].UnbannedAt.Should().BeNull();
    }

    [Fact]
    public async Task GetBanHistory_WithMultipleBans_ReturnsAllRecords()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));
        await SendAsync(new UnbanUserRequest(userId, Faker.Lorem.Sentence()));
        await SendAsync(new BanUserRequest(userId, Faker.Lorem.Sentence()));

        // Act
        var result = await SendAsync(new GetBanHistoryRequest(userId));

        // Assert
        result.Should().HaveCount(2);
        result[0].UnbannedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task GetBanHistory_WithNoBans_ReturnsEmptyList()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var result = await SendAsync(new GetBanHistoryRequest(userId));

        // Assert
        result.Should().BeEmpty();
    }
}
