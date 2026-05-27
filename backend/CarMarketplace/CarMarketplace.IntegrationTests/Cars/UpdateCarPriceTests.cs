using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.UpdateCarPrice;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdateCarPriceTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateCarPrice_WithValidData_ReturnsUpdatedCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("BMW", "M3", 2021, 200000m, "PLN", 30000, FuelType.Petrol, null));

        // Act
        var result = await SendAsync(new UpdateCarPriceRequest(carId, 180000m, "PLN"));

        // Assert
        result.Should().NotBeNull();
    }
}
