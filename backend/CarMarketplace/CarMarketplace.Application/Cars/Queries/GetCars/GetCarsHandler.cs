using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Domain.Cars;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

public class GetCarsHandler(
    ICarRepository carRepository) 
    : IRequestHandler<GetCarsRequest, IEnumerable<Car>>
{
    public async Task<IEnumerable<Car>> Handle(GetCarsRequest request, CancellationToken token)
    {
        // TODO
        var result = await carRepository.GetPagedAsync(request.PageNumber, request.PageSize, token);
        
        return result.Cars;
    }
}