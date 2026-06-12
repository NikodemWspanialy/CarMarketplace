using System.Net;
using Bogus;
using CarMarketplace.API.tests.Common;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.API.tests.Auth;

public class AdminPolicyTests(ApiTestFactory factory) : IClassFixture<ApiTestFactory>
{
    private readonly Faker _faker = new();

    [Fact]
    public async Task AdminEndpoint_WithoutToken_Returns401()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/admin/user/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task AdminEndpoint_WithRegularUserToken_Returns403()
    {
        // Arrange
        var client = factory.CreateClient();
        var email = _faker.Internet.Email();
        var password = _faker.Internet.Password();
        var token = await AuthHelper.RegisterAndLoginAsync(client, email, password);
        AuthHelper.SetBearerToken(client, token);

        // Act
        var response = await client.GetAsync($"/api/admin/user/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
