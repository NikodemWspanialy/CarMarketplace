using CarMarketplace.Application.Cars.Commands.UpdateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdateCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateCar_WithValidData_UpdatesInDb()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var newBrand = Faker.Vehicle.Manufacturer();
        var newModel = Faker.Vehicle.Model();

        // Act
        var result = await SendAsync(new UpdateCarRequest(carId, newBrand, newModel, 2023, 5000, FuelType.Diesel, "Updated"));

        // Assert
        result.Should().NotBeNull();
        result.Brand.Should().Be(newBrand);
        result.Model.Should().Be(newModel);

        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        car!.Brand.Should().Be(newBrand);
        car.Model.Should().Be(newModel);
    }

    [Fact]
    public async Task UpdateCar_WithNonExistingId_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new UpdateCarRequest(Guid.NewGuid(), Faker.Vehicle.Manufacturer(), Faker.Vehicle.Model(), 2022, 10000, FuelType.Petrol, null));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
