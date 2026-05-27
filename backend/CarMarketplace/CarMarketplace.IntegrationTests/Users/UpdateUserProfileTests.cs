using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class UpdateUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateProfile_WithValidData_ReturnsUpdatedProfile()
    {
        // Arrange
        var body = new { FirstName = "Updated", LastName = "Name" };

        // Act
        var response = await Client.PutAsJsonAsync("/api/user/update-profile", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<UserResponse>();
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("Updated");
        result.LastName.Should().Be("Name");
    }
}
