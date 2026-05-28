using CarMarketplace.Application.Authorization.Commands.RefreshToken;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class RefreshTokenTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task RefreshToken_WhenAuthenticated_ReturnsNewAccessToken()
    {
        // Act
        var result = await SendAsync(new RefreshTokenRequest());

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
    }
}
