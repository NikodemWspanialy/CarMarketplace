using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCar;

public class GetCarHandler(
    ICarRepository carRepository) 
    : IRequestHandler<GetCarRequest, Car?>
{
    public async Task<Car?> Handle(GetCarRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdAsync(request.Id, token);

        // TODO should be mapper - cant return aggregate

        return car ?? null;
    }
}