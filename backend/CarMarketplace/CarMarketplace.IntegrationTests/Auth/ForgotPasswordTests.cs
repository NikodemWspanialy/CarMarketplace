using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class ForgotPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task ForgotPassword_WithExistingEmail_ReturnsOk()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("forgot@example.com", "Password123!", "John", "Doe"));
        var body = new { Email = "forgot@example.com" };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/forgot-password", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
