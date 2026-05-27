using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class AdminUpdateUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task AdminUpdateProfile_WithValidData_ReturnsUpdatedUser()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("victim@example.com", "Password123!", "Old", "Name"));
        var body = new { UserId = userId, FirstName = "New", LastName = "Name" };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/admin/update-user-profile/{userId}", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<UserResponse>();
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("New");
    }
}
