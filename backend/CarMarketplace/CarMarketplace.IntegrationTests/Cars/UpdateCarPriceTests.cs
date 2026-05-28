using CarMarketplace.Application.Cars.Commands.UpdateCarPrice;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class UpdateCarPriceTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task UpdateCarPrice_WithValidData_UpdatesPrice()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        var newPrice = Faker.Random.Decimal(50000, 300000);

        // Act
        var result = await SendAsync(new UpdateCarPriceRequest(carId, newPrice, "EUR"));

        // Assert
        result.Should().NotBeNull();
        result.PriceAmount.Should().Be(newPrice);
        result.PriceCurrency.Should().Be("EUR");
    }

    [Fact]
    public async Task UpdateCarPrice_WithZeroAmount_ThrowsValidationException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new UpdateCarPriceRequest(carId, 0m, "PLN"));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task UpdateCarPrice_WithNegativeAmount_ThrowsValidationException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        var act = () => SendAsync(new UpdateCarPriceRequest(carId, -100m, "PLN"));

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
