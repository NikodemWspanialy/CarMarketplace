using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.CreateCar;

public class CreateCarHandler(
    //validator,
    //factory
    ICarRepository carRepository)
    : IRequestHandler<CreateCarRequest, Guid>
{
    public async Task<Guid> Handle(CreateCarRequest request, CancellationToken token)
    {
        // TODO Refactor, add validation - abstract and domain
        var car = new Car(
            Guid.NewGuid(),
            request.Brand,
            request.Model,
            request.Year,
            request.Price,
            request.Mileage,
            request.FuelType,
            request.Description
        );

        await carRepository.AddAsync(car, token);
        
        return car.Id;
    }
}