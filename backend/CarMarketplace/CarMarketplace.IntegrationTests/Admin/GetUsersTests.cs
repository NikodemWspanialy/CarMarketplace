using CarMarketplace.Application.Admin.Queries.GetUsers;
using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Admin;

public class GetUsersTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithAdminLogin(factory)
{
    [Fact]
    public async Task GetUsers_WithExistingUsers_ReturnsPagedList()
    {
        // Arrange
        await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));
        await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var result = await SendAsync(new GetUsersRequest(1, 10));

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThanOrEqualTo(2);
        result.TotalCount.Should().BeGreaterThanOrEqualTo(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task GetUsers_WithPagination_RespectsPageSize()
    {
        // Arrange
        for (var i = 0; i < 3; i++)
            await SendAsync(new RegisterUserRequest(Faker.Internet.Email(), Faker.Internet.Password(), Faker.Name.FirstName(), Faker.Name.LastName()));

        // Act
        var result = await SendAsync(new GetUsersRequest(1, 2));

        // Assert
        result.Items.Should().HaveCountLessThanOrEqualTo(2);
        result.PageSize.Should().Be(2);
    }
}
