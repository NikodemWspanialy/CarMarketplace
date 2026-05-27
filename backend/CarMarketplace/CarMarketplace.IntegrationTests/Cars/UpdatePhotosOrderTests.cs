using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdatePhotosOrderTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateOrder_WithValidData_ReordersPhotos()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Hyundai", "Tucson", 2022, 120000m, "PLN", 20000, FuelType.Diesel, null));
        var photo1 = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/1.jpg", 1, true));
        var photo2 = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/2.jpg", 2, false));

        var photos = new List<PhotoOrderItem>
        {
            new(photo1.Id, 2),
            new(photo2.Id, 1)
        };

        // Act
        await SendAsync(new UpdatePhotosOrderRequest(carId, photos));

        // Assert
        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.Should().HaveCount(2);
    }
}
