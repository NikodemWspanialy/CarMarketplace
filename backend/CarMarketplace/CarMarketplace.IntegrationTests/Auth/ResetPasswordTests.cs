using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Commands.ResetPassword;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class ResetPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task ResetPassword_WithValidToken_MarksTokenAsUsed()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest("reset@example.com", "Password123!", "John", "Doe"));
        await SendAsync(new ForgotPasswordRequest("reset@example.com"));

        var resetToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.IsUsed == false);

        // Act
        await SendAsync(new ResetPasswordRequest(resetToken!.Token, "NewPassword456!"));

        // Assert
        var updatedToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.Id == resetToken.Id);
        updatedToken!.IsUsed.Should().BeTrue();
    }
}
