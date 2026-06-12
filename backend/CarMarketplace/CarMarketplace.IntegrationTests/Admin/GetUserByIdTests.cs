using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Users.Queries.GetUserById;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class GetUserByIdTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task GetUserById_WithExistingUser_ReturnsUserResponse()
    {
        // Arrange
        var firstName = Faker.Name.FirstName();
        var lastName = Faker.Name.LastName();
        var email = Faker.Internet.Email();
        var userId = await SendAsync(new RegisterUserRequest(email, Faker.Internet.Password(), firstName, lastName));

        // Act
        var result = await SendAsync(new GetUserByIdRequest(userId));

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(firstName);
        result.LastName.Should().Be(lastName);
        result.Email.Should().Be(email);
    }

    [Fact]
    public async Task GetUserById_WithNonExistingUser_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new GetUserByIdRequest(Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
