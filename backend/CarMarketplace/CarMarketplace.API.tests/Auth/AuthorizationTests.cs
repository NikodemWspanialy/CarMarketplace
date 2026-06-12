using System.Net;
using System.Net.Http.Json;
using Bogus;
using CarMarketplace.API.tests.Common;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.API.tests.Auth;

public class AuthorizationTests(ApiTestFactory factory) : IClassFixture<ApiTestFactory>
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task CreateListing_WithoutToken_Returns401()
    {
        // Arrange
        var client = factory.CreateClient();

        var body = new { carId = Guid.NewGuid(), title = "Test", contactIds = new[] { Guid.NewGuid() } };

        // Act
        var response = await client.PostAsJsonAsync("/api/listing/create", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateListing_WithValidToken_DoesNotReturn401()
    {
        // Arrange
        var client = factory.CreateClient();
        var email = _faker.Internet.Email();
        var password = _faker.Internet.Password();
        var token = await AuthHelper.RegisterAndLoginAsync(client, email, password);
        AuthHelper.SetBearerToken(client, token);

        var body = new { carId = Guid.NewGuid(), title = "Test", contactIds = new[] { Guid.NewGuid() } };

        // Act
        var response = await client.PostAsJsonAsync("/api/listing/create", body);

        // Assert — should not be 401 (might be 400 due to invalid data, but not unauthorized)
        response.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
    }
}
