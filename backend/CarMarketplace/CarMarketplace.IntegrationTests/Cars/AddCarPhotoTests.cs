using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class AddCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhoto_WithValidData_PersistsPhotoOnCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var url = Faker.Internet.Url();

        // Act
        var result = await SendAsync(new AddCarPhotoRequest(carId, url, 1, true));

        // Assert
        result.Should().NotBeNull();
        result.Url.Should().Be(url);
        result.IsPrimary.Should().BeTrue();

        var car = await SendAsync(new GetCarRequest(carId));
        car.Photos.Should().HaveCount(1);
    }

    [Fact]
    public async Task AddPhoto_WithEmptyUrl_ThrowsValidationException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new AddCarPhotoRequest(carId, "", 1, true));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AddPhoto_WithInvalidUrl_ThrowsValidationException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new AddCarPhotoRequest(carId, "not-a-url", 1, true));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task AddPhoto_WithNonExistingCar_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new AddCarPhotoRequest(Guid.NewGuid(), Faker.Internet.Url(), 1, true));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}
