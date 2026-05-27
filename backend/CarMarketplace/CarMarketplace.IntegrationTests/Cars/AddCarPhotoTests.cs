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

public class AddCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhoto_WithValidData_ReturnsCreated()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Volvo", "XC60", 2022, 180000m, "PLN", 10000, FuelType.Diesel, null));
        var body = new { Url = "https://example.com/photo.jpg", Order = 1, IsPrimary = true };

        // Act
        var response = await Client.PostAsJsonAsync($"/api/car/{carId}/photos", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<CarPhotoResponse>();
        result.Should().NotBeNull();
        result!.Url.Should().Be("https://example.com/photo.jpg");
    }
}
