using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Auth;

public class LoginUserTests(CarMarketplaceApiFactory factory) : IntegrationTestBase(factory)
{
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsAccessToken()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var password = Faker.Internet.Password();
        await SendAsync(new RegisterUserRequest(email, password, Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var result = await SendAsync(new LoginUserQuery(email, password));

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Login_WithWrongPassword_ThrowsException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new LoginUserQuery(email, "WrongPassword"));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task Login_WithNonExistingEmail_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new LoginUserQuery(Faker.Internet.Email(), Faker.Internet.Password()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task Login_WithEmptyEmail_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new LoginUserQuery("", Faker.Internet.Password()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Login_WithEmptyPassword_ThrowsValidationException()
    {
        // Act
        var act = () => SendAsync(new LoginUserQuery(Faker.Internet.Email(), ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
