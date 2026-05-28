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
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new ForgotPasswordRequest(email));

        // Assert
        var token = await TestData.PasswordResetTokens.FirstOrDefaultAsync();
        token.Should().NotBeNull();
        token!.IsUsed.Should().BeFalse();
        token.ExpiresAt.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public async Task ForgotPassword_WithNonExistingEmail_DoesNotThrow()
    {
        // Act — should not reveal if email exists
        var act = () => SendAsync(new ForgotPasswordRequest(Faker.Internet.Email()));

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ForgotPassword_CalledTwice_CreatesTwoTokens()
    {
        // Arrange
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new ForgotPasswordRequest(email));
        await SendAsync(new ForgotPasswordRequest(email));

        // Assert
        var tokens = await TestData.PasswordResetTokens.ToListAsync();
        tokens.Should().HaveCount(2);
    }
}
