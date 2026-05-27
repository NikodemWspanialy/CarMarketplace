using System.Net;
using System.Net.Http.Json;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Domain.Cars;
using CarMarketplace.IntegrationTests.Common;
using CarMarketplace.IntegrationTests.Common.IntegrationTestBases;
using FluentAssertions;
using Xunit;

namespace CarMarketplace.IntegrationTests.Cars;

public class GetCarsTests(CarMarketplaceApiFactory factory) : IntegrationTestBaseWithUserLogin(factory)
{
    [Fact]
    public async Task GetCars_WithExistingCars_ReturnsPagedList()
    {
        // Arrange
        await SendAsync(new CreateCarRequest("Toyota", "Yaris", 2020, 60000m, "PLN", 40000, FuelType.Petrol, null));
        await SendAsync(new CreateCarRequest("Honda", "Civic", 2021, 80000m, "PLN", 20000, FuelType.Petrol, null));

        // Act
        var response = await Client.GetAsync("/api/car/get-details-list?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CarListResponse>();
        result.Should().NotBeNull();
        result!.Items.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}
