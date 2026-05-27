using CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class AdminUpdateUserProfileTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task AdminUpdateProfile_WithValidData_ReturnsUpdatedUser()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        var newFirstName = Faker.Name.FirstName();
        var newLastName = Faker.Name.LastName();

        // Act
        var result = await SendAsync(new AdminUpdateUserProfileRequest(userId, newFirstName, newLastName));

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(newFirstName);
        result.LastName.Should().Be(newLastName);
    }

    [Fact]
    public async Task AdminUpdateProfile_WithEmptyFirstName_ThrowsValidationException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new AdminUpdateUserProfileRequest(userId, "", Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AdminUpdateProfile_WithNullFirstName_ThrowsValidationException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new AdminUpdateUserProfileRequest(userId, null!, Faker.Name.LastName()));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AdminUpdateProfile_WithEmptyLastName_ThrowsValidationException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new AdminUpdateUserProfileRequest(userId, Faker.Name.FirstName(), ""));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AdminUpdateProfile_WithNullLastName_ThrowsValidationException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new AdminUpdateUserProfileRequest(userId, Faker.Name.FirstName(), null!));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
