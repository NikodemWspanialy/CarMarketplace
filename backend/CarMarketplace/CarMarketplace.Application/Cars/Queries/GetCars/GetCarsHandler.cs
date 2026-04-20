using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

internal class GetCarsHandler(
    ICarRepository carRepository) 
    : IRequestHandler<GetCarsRequest, CarListResponse>
{
    public async Task<CarListResponse> Handle(GetCarsRequest request, CancellationToken token)
    {
        var result = await carRepository.GetPagedAsync(request.PageNumber, request.PageSize, token);
        var items = result.Cars.Select(CarResponse.FromEntity).ToList();

        return new CarListResponse(items, result.TotalCount);
    }
}