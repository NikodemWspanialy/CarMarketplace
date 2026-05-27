using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Users.DTOs;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class GetUserByIdTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task GetById_WithExistingUser_ReturnsUserResponse()
    {
        // Arrange
        var userId = await SendAsync(new RegisterUserRequest("user@example.com", "Password123!", "John", "Doe"));

        // Act
        var response = await Client.GetAsync($"/api/user/{userId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var user = await response.Content.ReadFromJsonAsync<UserResponse>();
        user.Should().NotBeNull();
        user!.FirstName.Should().Be("John");
    }
}
