using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class DeleteCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task DeletePhoto_WhenExists_RemovesFromCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photo = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 1, true));

        // Act
        await SendAsync(new DeleteCarPhotoRequest(carId, photo.Id));

        // Assert
        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.Should().BeEmpty();
    }

    [Fact]
    public async Task DeletePhoto_WithNonExistingPhotoId_ThrowsException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new DeleteCarPhotoRequest(carId, Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
