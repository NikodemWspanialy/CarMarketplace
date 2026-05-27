using System.Net;
using CarMarketplace.Application.Cars.Commands.CreateCar;
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
    public async Task DeleteCar_WhenOwner_ReturnsNoContent()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Audi", "A4", 2020, 120000m, "PLN", 50000, FuelType.Diesel, null));

        // Act
        var response = await Client.DeleteAsync($"/api/car/delete/{carId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        car!.IsDeleted.Should().BeTrue();
    }
}
