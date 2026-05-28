using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class CreateCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task CreateCar_WithValidData_PersistsCarInDb()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().Build();

        // Act
        var carId = await SendAsync(command);

        // Assert
        carId.Should().NotBeEmpty();
        var car = await TestData.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        car.Should().NotBeNull();
        car!.Brand.Should().Be(command.Brand);
        car.Model.Should().Be(command.Model);
        car.Year.Should().Be(command.Year);
        car.Mileage.Should().Be(command.Mileage);
        car.IsDeleted.Should().BeFalse();
    }

    [Fact]
    public async Task CreateCar_WithEmptyBrand_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().WithBrand("").Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateCar_WithZeroPrice_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().WithPrice(0m).Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateCar_WithNegativeMileage_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().WithMileage(-1).Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateCar_WithInvalidYear_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().WithYear(1800).Build();

        // Act
        var act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
