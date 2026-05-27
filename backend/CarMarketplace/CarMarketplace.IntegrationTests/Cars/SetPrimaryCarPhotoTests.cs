using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class SetPrimaryCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task SetPrimary_WithExistingPhoto_ReturnsUpdatedPhoto()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Kia", "Sportage", 2023, 130000m, "PLN", 5000, FuelType.Hybrid, null));
        var photo = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/photo.jpg", 1, false));

        // Act
        var response = await Client.PutAsync($"/api/car/{carId}/photos/{photo.Id}/set-primary", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CarPhotoResponse>();
        result.Should().NotBeNull();
        result!.IsPrimary.Should().BeTrue();
    }
}
