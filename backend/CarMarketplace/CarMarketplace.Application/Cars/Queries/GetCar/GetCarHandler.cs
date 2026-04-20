using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Exceptions;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Queries.GetCar;

internal class GetCarHandler(
    ICarRepository carRepository) 
    : IRequestHandler<GetCarRequest, CarResponse>
{
    public async Task<CarResponse> Handle(GetCarRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdReadOnlyAsync(request.Id, token)
            ?? throw new CarNotFoundException(request.Id);

        return CarResponse.FromEntity(car);
    }
}