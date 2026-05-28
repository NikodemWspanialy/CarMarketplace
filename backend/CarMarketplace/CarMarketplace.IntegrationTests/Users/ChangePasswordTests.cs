using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.Application.Users.Commands.ChangePassword;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class ChangePasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task ChangePassword_WithValidOldPassword_AllowsLoginWithNewPassword()
    {
        // Arrange
        var newPassword = Faker.Random.AlphaNumeric(8);

        // Act
        await SendAsync(new ChangePasswordRequest(UserPassword, newPassword));

        // Assert
        var result = await SendAsync(new LoginUserQuery(UserEmail, newPassword));
        result.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ChangePassword_WithWrongOldPassword_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new ChangePasswordRequest(Faker.Random.AlphaNumeric(8), Faker.Random.AlphaNumeric(8)));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task ChangePassword_WithEmptyOldPassword_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new ChangePasswordRequest("", Faker.Random.AlphaNumeric(8)));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ChangePassword_WithTooShortNewPassword_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new ChangePasswordRequest(UserPassword, "short"));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ChangePassword_WithSamePassword_ThrowsDomainException()
    {
        // Act
        var act = () => SendAsync(new ChangePasswordRequest(UserPassword, UserPassword));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
