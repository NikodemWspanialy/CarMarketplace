using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Common.Interfaces;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.CreateCar;

internal class CreateCarHandler(
    ICarFactory carFactory,
    ICarRepository carRepository,
    ICurrentUserProvider currentUserProvider)
    : IRequestHandler<CreateCarRequest, Guid>
{
    public async Task<Guid> Handle(CreateCarRequest request, CancellationToken token)
    {
        var sellerId = currentUserProvider.GetUserId();
        var car = carFactory.Create(request, sellerId);

        await carRepository.AddAsync(car, token);
        
        return car.Id;
    }
}