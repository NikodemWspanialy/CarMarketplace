using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class AddCarPhotoTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task AddPhoto_WithValidData_ReturnsPhotoResponse()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequest("Volvo", "XC60", 2022, 180000m, "PLN", 10000, FuelType.Diesel, null));

        // Act
        var result = await SendAsync(new AddCarPhotoRequest(carId, "https://example.com/photo.jpg", 1, true));

        // Assert
        result.Should().NotBeNull();
        result.Url.Should().Be("https://example.com/photo.jpg");
        result.IsPrimary.Should().BeTrue();
    }
}
