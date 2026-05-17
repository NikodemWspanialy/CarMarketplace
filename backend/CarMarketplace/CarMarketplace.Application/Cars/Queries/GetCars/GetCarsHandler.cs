using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Common.DTOs;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

internal class GetCarsHandler(
    ICarRepository carRepository)
    : IRequestHandler<GetCarsRequest, ListResponse<CarResponse>>
{
    public async Task<ListResponse<CarResponse>> Handle(GetCarsRequest request, CancellationToken token)
    {
        var result = await carRepository.GetPagedAsync(request.PageNumber, request.PageSize, token);
        var items = result.Cars.Select(CarResponse.FromEntity).ToList();

        return new ListResponse<CarResponse>(items, result.TotalCount, request.PageNumber, request.PageSize);
    }
}
