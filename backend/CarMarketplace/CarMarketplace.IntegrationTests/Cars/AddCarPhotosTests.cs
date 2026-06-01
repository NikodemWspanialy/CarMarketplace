using CarMarketplace.Application.Cars.Commands.AddCarPhotos;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class AddCarPhotosTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhotos_WithValidBatch_PersistsAllPhotos()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photos = new List<AddCarPhotosItem>
        {
            new AddCarPhotosItemBuilder().AsPrimary().WithOrder(1).Build(),
            new AddCarPhotosItemBuilder().WithOrder(2).Build(),
            new AddCarPhotosItemBuilder().WithOrder(3).Build()
        };

        // Act
        var result = await SendAsync(new AddCarPhotosRequest(carId, photos));

        // Assert
        result.Should().HaveCount(3);

        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.Should().HaveCount(3);
    }

    [Fact]
    public async Task AddPhotos_WithEmptyList_ThrowsException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new AddCarPhotosRequest(carId, []));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AddPhotos_WithMultiplePrimary_ThrowsException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var photos = new List<AddCarPhotosItem>
        {
            new AddCarPhotosItemBuilder().AsPrimary().WithOrder(1).Build(),
            new AddCarPhotosItemBuilder().AsPrimary().WithOrder(2).Build()
        };

        // Act
        var act = () => SendAsync(new AddCarPhotosRequest(carId, photos));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
