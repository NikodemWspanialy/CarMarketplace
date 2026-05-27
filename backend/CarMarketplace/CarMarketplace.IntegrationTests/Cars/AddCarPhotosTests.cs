using CarMarketplace.Application.Cars.Commands.AddCarPhotos;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class AddCarPhotosTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhotos_WithValidBatch_ReturnsAllPhotos()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Ford", "Focus", 2021, 70000m, "PLN", 30000, FuelType.Petrol, null));
        var photos = new List<AddCarPhotosItem>
        {
            new("https://example.com/1.jpg", 1, true),
            new("https://example.com/2.jpg", 2, false)
        };

        // Act
        var result = await SendAsync(new AddCarPhotosRequest(carId, photos));

        // Assert
        result.Should().HaveCount(2);
    }
}
