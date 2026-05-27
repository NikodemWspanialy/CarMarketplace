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

public class GetCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCar_WithExistingId_ReturnsCarDetails()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Mercedes", "C200", 2021, 150000m, "PLN", 25000, FuelType.Petrol, "Nice car"));

        // Act
        var response = await Client.GetAsync($"/api/car/get-details/{carId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CarDetailsResponse>();
        result.Should().NotBeNull();
        result!.Brand.Should().Be("Mercedes");
    }
}
