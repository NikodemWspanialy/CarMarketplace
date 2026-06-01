using System.ComponentModel.DataAnnotations;
using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdatePhotosOrderTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateOrder_WithValidData_ReordersPhotos()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photo1 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 1, true));
        var photo2 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 2, false));

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
        car.Photos[0].Id.Should().Be(photo2.Id);
        car.Photos[1].Id.Should().Be(photo1.Id);
    }

    [Fact]
    public async Task UpdateOrder_WithNonExistingCar_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new UpdatePhotosOrderRequest(Guid.NewGuid(), [new(Guid.NewGuid(), 1)]));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateOrder_WithDuplicateOrder_ThrowsValidationException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photo1 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 1, true));
        var photo2 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 2, false));

        var photos = new List<PhotoOrderItem>
        {
            new(photo1.Id, 1),
            new(photo2.Id, 1)
        };

        // Act
        var act = () => SendAsync(new UpdatePhotosOrderRequest(carId, photos));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
