using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class AdminChangeUserPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task AdminChangePassword_WithValidData_ReturnsNoContent()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("user@example.com", "Password123!", "John", "Doe"));
        var body = new { UserId = userId, NewPassword = "AdminChanged456!" };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/admin/change-user-password/{userId}", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
