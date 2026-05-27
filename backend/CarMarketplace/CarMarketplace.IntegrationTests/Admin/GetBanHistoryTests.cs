using CarMarketplace.Application.Admin.Commands.BanUser;
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
        var userId = await SendAsync(new RegisterUserRequest("history@example.com", "Password123!", "History", "User"));
        await SendAsync(new BanUserRequest(userId, "First offense"));

        // Act
        var result = await SendAsync(new GetBanHistoryRequest(userId));

        // Assert
        result.Should().HaveCount(1);
        result[0].Reason.Should().Be("First offense");
    }
}
