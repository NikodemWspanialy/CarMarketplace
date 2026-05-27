using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class CreateCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task CreateCar_WithValidData_PersistsCarInDb()
    {
        // Act
        var carId = await SendAsync(new CreateCarRequest("Toyota", "Corolla", 2022, 85000m, "PLN", 15000, FuelType.Petrol, "Well maintained"));

        // Assert
        carId.Should().NotBeEmpty();
        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        car.Should().NotBeNull();
        car!.Brand.Should().Be("Toyota");
    }
}
