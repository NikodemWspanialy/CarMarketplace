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

public class AddCarPhotosTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhotos_WithValidBatch_ReturnsCreated()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Ford", "Focus", 2021, 70000m, "PLN", 30000, FuelType.Petrol, null));
        var body = new
        {
            Photos = new[]
            {
                new { Url = "https://example.com/photo1.jpg", Order = 1, IsPrimary = true },
                new { Url = "https://example.com/photo2.jpg", Order = 2, IsPrimary = false }
            }
        };

        // Act
        var response = await Client.PostAsJsonAsync($"/api/car/{carId}/photos/batch", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var result = await response.Content.ReadFromJsonAsync<List<CarPhotoResponse>>();
        result.Should().HaveCount(2);
    }
}
