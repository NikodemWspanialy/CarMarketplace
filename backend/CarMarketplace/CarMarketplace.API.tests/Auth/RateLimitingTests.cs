using System.Net;
using System.Net.Http.Json;
using CarMarketplace.API.tests.Common;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.API.tests.Auth;

public class RateLimitingTests(ApiTestFactory factory) : IClassFixture<ApiTestFactory>
{
    [Fact]
    public async Task Login_ExceedingRateLimit_Returns429()
    {
        // Arrange
        var client = factory.CreateClient();

        var loginBody = new { email = "rate@test.com", password = "password123" };

        // Act — send 6 requests (limit is 5 per minute)
        var responses = new List<HttpResponseMessage>();
        for (var i = 0; i < 6; i++)
            responses.Add(await client.PostAsJsonAsync("/api/auth/login", loginBody));

        // Assert — at least the 6th should be 429
        responses.Last().StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
    }

    [Fact]
    public async Task ForgotPassword_ExceedingRateLimit_Returns429()
    {
        // Arrange
        var client = factory.CreateClient();

        var body = new { email = "forgot@test.com" };

        // Act — send 6 requests
        var responses = new List<HttpResponseMessage>();
        for (var i = 0; i < 6; i++)
            responses.Add(await client.PostAsJsonAsync("/api/auth/forgot-password", body));

        // Assert
        responses.Last().StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
    }
}
