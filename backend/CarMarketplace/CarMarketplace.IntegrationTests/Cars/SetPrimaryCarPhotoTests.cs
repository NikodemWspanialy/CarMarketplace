using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class SetPrimaryCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task SetPrimary_WithExistingPhoto_ReturnsPrimaryPhoto()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Kia", "Sportage", 2023, 130000m, "PLN", 5000, FuelType.Hybrid, null));
        var photo = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/photo.jpg", 1, false));

        // Act
        var result = await SendAsync(new SetPrimaryCarPhotoRequest(carId, photo.Id));

        // Assert
        result.Should().NotBeNull();
        result.IsPrimary.Should().BeTrue();
    }
}
