using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class ResetPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task ResetPassword_WithValidToken_ReturnsNoContent()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("reset@example.com", "Password123!", "John", "Doe"));
        await SendAsync(new ForgotPasswordRequest("reset@example.com"));

        // Get the token from DB
        var resetToken = await TestData.PasswordResetTokens
            .FirstOrDefaultAsync(t => t.IsUsed == false);

        var body = new { Token = resetToken!.Token, NewPassword = "NewPassword456!" };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/reset-password", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
