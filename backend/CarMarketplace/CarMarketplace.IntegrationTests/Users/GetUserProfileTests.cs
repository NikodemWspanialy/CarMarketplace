using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class GetUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetProfile_WhenAuthenticated_ReturnsCurrentUserProfile()
    {
        // Act
        var response = await Client.GetAsync("/api/user/profile");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var profile = await response.Content.ReadFromJsonAsync<UserResponse>();
        profile.Should().NotBeNull();
        profile!.Email.Should().Be("user@test.com");
    }
}
