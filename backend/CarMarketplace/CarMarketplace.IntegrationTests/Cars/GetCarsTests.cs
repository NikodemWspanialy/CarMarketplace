using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Queries.GetCars;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class GetCarsTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCars_WithExistingCars_ReturnsPagedList()
    {
        // Arrange
        await SendAsync(new CreateCarRequest("Toyota", "Yaris", 2020, 60000m, "PLN", 40000, FuelType.Petrol, null));
        await SendAsync(new CreateCarRequest("Honda", "Civic", 2021, 80000m, "PLN", 20000, FuelType.Petrol, null));

        // Act
        var result = await SendAsync(new GetCarsRequest(1, 10));

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}
