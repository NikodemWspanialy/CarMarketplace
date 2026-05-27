using CarMarketplace.Application.Cars.Queries.GetCar;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using CarMarketplace.Tests.Shared.Builders.Car;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class GetCarTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCar_WithExistingId_ReturnsFullDetails()
    {
        // Arrange
        var command = new CreateCarRequestBuilder().Build();
        var carId = await SendAsync(command);

        // Act
        var result = await SendAsync(new GetCarRequest(carId));

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(carId);
        result.Brand.Should().Be(command.Brand);
        result.Model.Should().Be(command.Model);
        result.SellerId.Should().Be(UserId);
    }

    [Fact]
    public async Task GetCar_WithNonExistingId_ThrowsException()
    {
        // Act
        var act = () => SendAsync(new GetCarRequest(Guid.NewGuid()));

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
