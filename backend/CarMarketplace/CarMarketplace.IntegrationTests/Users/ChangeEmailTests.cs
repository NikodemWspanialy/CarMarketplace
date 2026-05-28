using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Users.Commands.ChangeEmail;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Users;

public class ChangeEmailTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task ChangeEmail_WithValidEmail_UpdatesInDb()
    {
        // Arrange
        var newEmail = Faker.Internet.Email();

        // Act
        await SendAsync(new ChangeEmailRequest(newEmail));

        // Assert
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.Email.Should().Be(newEmail);
    }

    [Fact]
    public async Task ChangeEmail_WithSameEmail_ThrowsDomainException()
    {
        // Act
        var act = () => SendAsync(new ChangeEmailRequest(UserEmail));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task ChangeEmail_WithEmptyEmail_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new ChangeEmailRequest(""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();

        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        user!.Email.Should().Be(UserEmail);
    }

    [Fact]
    public async Task ChangeEmail_WithInvalidFormat_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new ChangeEmailRequest("not-an-email"));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task ChangeEmail_WithEmailTakenByAnotherUser_ThrowsException()
    {
        // Arrange
        var otherEmail = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(otherEmail, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new ChangeEmailRequest(otherEmail));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
