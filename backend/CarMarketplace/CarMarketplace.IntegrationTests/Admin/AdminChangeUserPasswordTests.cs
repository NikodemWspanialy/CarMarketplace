using CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Queries.LoginUser;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class AdminChangeUserPasswordTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task AdminChangePassword_WithValidData_AllowsLoginWithNewPassword()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var oldPassword = Faker.Internet.Password();
        var newPassword = Faker.Internet.Password();

        var userId = await SendAsync(new RegisterUserRequest(email, oldPassword, Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        await SendAsync(new AdminChangeUserPasswordRequest(userId, newPassword));

        // Assert
        var result = await SendAsync(new LoginUserQuery(email, newPassword));
        result.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task AdminChangePassword_WithSamePassword_ThrowsDomainException()
    {
        // Arrange
        var email = Faker.Internet.Email();
        var password = Faker.Internet.Password();

        var userId = await SendAsync(new RegisterUserRequest(email, password, Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var act = () => SendAsync(new AdminChangeUserPasswordRequest(userId, password));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
