using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class DeleteCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task DeletePhoto_WhenExists_RemovesPhoto()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Mazda", "CX-5", 2022, 140000m, "PLN", 15000, FuelType.Petrol, null));
        var photo = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/photo.jpg", 1, true));

        // Act
        await SendAsync(new DeleteCarPhotoRequest(carId, photo.Id));

        // Assert
        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.Should().BeEmpty();
    }
}
