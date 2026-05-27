using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdatePhotosOrderTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateOrder_WithValidData_ReturnsNoContent()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Hyundai", "Tucson", 2022, 120000m, "PLN", 20000, FuelType.Diesel, null));
        var photo1 = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/1.jpg", 1, true));
        var photo2 = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/2.jpg", 2, false));

        var body = new
        {
            Photos = new[]
            {
                new { Id = photo1.Id, NewOrder = 2 },
                new { Id = photo2.Id, NewOrder = 1 }
            }
        };

        // Act
        var response = await Client.PutAsJsonAsync($"/api/car/{carId}/photos/update-order", body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
