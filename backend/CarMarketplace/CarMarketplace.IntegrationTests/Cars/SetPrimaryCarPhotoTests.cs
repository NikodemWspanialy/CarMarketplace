using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class SetPrimaryCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task SetPrimary_WithExistingPhoto_MakesItPrimary()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photo1 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 1, true));
        var photo2 = await SendAsync(new AddCarPhotoRequest(carId, Faker.Internet.Url(), 2, false));

        // Act
        var result = await SendAsync(new SetPrimaryCarPhotoRequest(carId, photo2.Id));

        // Assert
        result.Should().NotBeNull();
        result.IsPrimary.Should().BeTrue();

        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.First(p => p.Id == photo2.Id).IsPrimary.Should().BeTrue();
        car.Photos.First(p => p.Id == photo1.Id).IsPrimary.Should().BeFalse();
    }

    [Fact]
    public async Task SetPrimary_WithNonExistingPhotoId_ThrowsException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new SetPrimaryCarPhotoRequest(carId, Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
