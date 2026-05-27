using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class GetCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCar_WithExistingId_ReturnsCarDetails()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Mercedes", "C200", 2021, 150000m, "PLN", 25000, FuelType.Petrol, "Nice car"));

        // Act
        var result = await SendAsync(new GetCarRequest(carId));

        // Assert
        result.Should().NotBeNull();
        result.Brand.Should().Be("Mercedes");
    }
}
