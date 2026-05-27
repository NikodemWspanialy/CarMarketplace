using System.Net;
using System.Net.Http.Json;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class ChangePasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task ChangePassword_WithValidOldPassword_ReturnsNoContent()
    {
        // Arrange
        var body = new { OldPassword = "TestPassword123!", NewPassword = "NewStrongPassword456!" };

        // Act
        var response = await Client.PutAsJsonAsync("/api/user/change-password", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
