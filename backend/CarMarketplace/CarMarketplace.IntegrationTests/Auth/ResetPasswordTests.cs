using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Commands.ResetPassword;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
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
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new ForgotPasswordRequest(email));

        var resetToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.IsUsed == false);

        // Act
        await SendAsync(new ResetPasswordRequest(resetToken!.Token, Faker.Internet.Password()));

        // Assert
        var updatedToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.Id == resetToken.Id);
        updatedToken!.IsUsed.Should().BeTrue();
    }

    [Fact]
    public async Task ResetPassword_WithValidToken_AllowsLoginWithNewPassword()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var newPassword = Faker.Internet.Password();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new ForgotPasswordRequest(email));

        var resetToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.IsUsed == false);

        // Act
        await SendAsync(new ResetPasswordRequest(resetToken!.Token, newPassword));

        // Assert
        var result = await SendAsync(new LoginUserQuery(email, newPassword));
        result.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ResetPassword_WithUsedToken_ThrowsException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new ForgotPasswordRequest(email));

        var resetToken = await TestData.PasswordResetTokens.FirstOrDefaultAsync(t => t.IsUsed == false);
        await SendAsync(new ResetPasswordRequest(resetToken!.Token, Faker.Internet.Password()));

        // Act
        var act = () => SendAsync(new ResetPasswordRequest(resetToken.Token, Faker.Internet.Password()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task ResetPassword_WithInvalidToken_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new ResetPasswordRequest("invalid-token", Faker.Internet.Password()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
