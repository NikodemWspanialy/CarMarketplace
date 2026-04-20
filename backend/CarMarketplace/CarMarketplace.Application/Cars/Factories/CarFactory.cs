using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;
using CarMarketplace.Domain.Common;

namespace CarMarketplace.Application.Cars.Factories;

internal interface ICarFactory
{
    Car Create(CreateCarRequest request, List<CarPhoto>? photos = null);
}

internal class CarFactory : ICarFactory
{
    public Car Create(CreateCarRequest request, List<CarPhoto>? photos = null) =>
        new Car(
            id: Guid.NewGuid(),
            brand: request.Brand,
            model: request.Model,
            year: request.Year,
            price: new Money(request.PriceAmount, request.PriceCurrency),
            mileage: request.Mileage,
            fuelType: request.FuelType,
            description: request.Description,
            photos: photos ?? []);
}
