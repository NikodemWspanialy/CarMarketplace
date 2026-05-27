using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class RegisterUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task Register_WithValidData_CreatesUserInDb()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var firstName = Faker.Name.FirstName();
        var lastName = Faker.Name.LastName();

        // Act
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), firstName, lastName));

        // Assert
        userId.Should().NotBeEmpty();
        var user = await TestData.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user.Should().NotBeNull();
        user!.Email.Should().Be(email);
        user.FirstName.Should().Be(firstName);
        user.LastName.Should().Be(lastName);
        user.Role.Should().Be(UserRole.User);
        user.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_ThrowsException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Register_WithEmptyEmail_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new RegisterUserRequest("", Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Register_WithInvalidEmail_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new RegisterUserRequest("not-an-email", Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Register_WithEmptyPassword_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new RegisterUserRequest(Faker.Internet.Email(), "", Faker.Name.FirstName(), Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Register_WithEmptyFirstName_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), "", Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Register_WithEmptyLastName_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
