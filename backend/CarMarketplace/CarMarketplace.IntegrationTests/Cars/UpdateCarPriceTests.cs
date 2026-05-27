using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.DTOs;
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
        var body = new { Id = carId, PriceAmount = 180000m, PriceCurrency = "PLN" };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/car/update-price/{carId}", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CarDetailsResponse>();
        result.Should().NotBeNull();
    }
}
