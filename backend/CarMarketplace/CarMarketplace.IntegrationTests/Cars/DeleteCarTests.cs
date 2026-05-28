using CarMarketplace.Application.Cars.Commands.DeleteCar;
using CarMarketplace.Domain.Exceptions;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class DeleteCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task DeleteCar_WhenOwner_SoftDeletesCar()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());

        // Act
        await SendAsync(new DeleteCarRequest(carId));

        // Assert
        var car = await TestData.Cars.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Id == carId);
        car.Should().NotBeNull();
        car!.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCar_WhenAlreadyDeleted_ThrowsDomainException()
    {
        // Arrange
        var carId = await SendAsync(new CreateCarRequestBuilder().Build());
        await SendAsync(new DeleteCarRequest(carId));

        // Act
        var act = () => SendAsync(new DeleteCarRequest(carId));

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task DeleteCar_WithNonExistingId_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new DeleteCarRequest(Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
