using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.CreateCar;

internal class CreateCarHandler(
    ICarFactory carFactory,
    ICarRepository carRepository)
    : IRequestHandler<CreateCarRequest, Guid>
{
    public async Task<Guid> Handle(CreateCarRequest request, CancellationToken token)
    {
        var car = carFactory.Create(request);

        await carRepository.AddAsync(car, token);
        
        return car.Id;
    }
}