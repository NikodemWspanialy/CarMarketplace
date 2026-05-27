using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.UpdateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdateCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateCar_WithValidData_ReturnsUpdatedDetails()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Toyota", "Corolla", 2022, 85000m, "PLN", 15000, FuelType.Petrol, null));

        // Act
        var result = await SendAsync(new UpdateCarRequest(carId, "Toyota", "Camry", 2023, 5000, FuelType.Petrol, "Updated"));

        // Assert
        result.Should().NotBeNull();
        result.Model.Should().Be("Camry");
    }
}
