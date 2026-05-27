using CarMarketplace.Application.Cars.Queries.GetCars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class GetCarsTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCars_WithExistingCars_ReturnsPagedList()
    {
        // Arrange
        await SendAsync(new CreateCarRequestBuilder().Build());
        await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var result = await SendAsync(new GetCarsRequest(1, 10));

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThanOrEqualTo(2);
        result.TotalCount.Should().BeGreaterThanOrEqualTo(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
    }

    [Fact]
    public async Task GetCars_WithPagination_RespectsPageSize()
    {
        // Arrange
        for (var i = 0; i < 3; i++)
            await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var result = await SendAsync(new GetCarsRequest(1, 2));

        // Assert
        result.Items.Should().HaveCountLessThanOrEqualTo(2);
        result.PageSize.Should().Be(2);
    }

    [Fact]
    public async Task GetCars_WhenEmpty_ReturnsEmptyList()
    {
        // Act
        var result = await SendAsync(new GetCarsRequest(1, 10));

        // Assert
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }
}
