using System.Net;
using System.Net.Http.Json;
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
    public async Task CreateCar_WithValidData_ReturnsCreated()
    {
        // Arrange
        var body = new
        {
            Brand = "Toyota",
            Model = "Corolla",
            Year = 2022,
            PriceAmount = 85000m,
            PriceCurrency = "PLN",
            Mileage = 15000,
            FuelType = FuelType.Petrol,
            Description = "Well maintained"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/car/create", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Brand == "Toyota");
        car.Should().NotBeNull();
    }
}
