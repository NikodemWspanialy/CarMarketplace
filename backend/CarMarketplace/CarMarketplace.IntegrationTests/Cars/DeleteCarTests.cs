using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.DeleteCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class DeleteCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task DeleteCar_WhenOwner_SoftDeletesCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Audi", "A4", 2020, 120000m, "PLN", 50000, FuelType.Diesel, null));

        // Act
        await SendAsync(new DeleteCarRequest(carId));

        // Assert
        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        car!.IsDeleted.Should().BeTrue();
    }
}
