using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.DTOs;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class RefreshTokenTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task RefreshToken_WhenAuthenticated_ReturnsNewToken()
    {
        // Act
        var response = await Client.PostAsync("/api/auth/refresh-token", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        result.Should().NotBeNull();
        result!.AccessToken.Should().NotBeNullOrEmpty();
    }
}
