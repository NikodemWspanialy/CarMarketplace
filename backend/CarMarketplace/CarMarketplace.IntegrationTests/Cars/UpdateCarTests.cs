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

public class UpdateCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateCar_WithValidData_ReturnsUpdatedCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Toyota", "Corolla", 2022, 85000m, "PLN", 15000, FuelType.Petrol, null));
        var body = new
        {
            Id = carId,
            Brand = "Toyota",
            Model = "Camry",
            Year = 2023,
            Mileage = 5000,
            FuelType = FuelType.Petrol,
            Description = "Updated description"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/car/update-details/{carId}", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CarDetailsResponse>();
        result.Should().NotBeNull();
        result!.Model.Should().Be("Camry");
    }
}
