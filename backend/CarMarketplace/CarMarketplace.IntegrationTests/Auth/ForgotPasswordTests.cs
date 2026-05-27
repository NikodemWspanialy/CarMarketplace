using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class ForgotPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task ForgotPassword_WithExistingEmail_CreatesResetToken()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("forgot@example.com", "Password123!", "John", "Doe"));

        // Act
        await SendAsync(new ForgotPasswordRequest("forgot@example.com"));

        // Assert
        var token = await TestData.PasswordResetTokens.FirstOrDefaultAsync();
        token.Should().NotBeNull();
        token!.IsUsed.Should().BeFalse();
    }
}
