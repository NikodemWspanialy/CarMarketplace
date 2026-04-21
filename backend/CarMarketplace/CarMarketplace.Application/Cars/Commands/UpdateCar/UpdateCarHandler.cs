using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdateCar;

internal class UpdateCarHandler(
    ICarSearcher carSearcher,
    ICarRepository carRepository) : IRequestHandler<UpdateCarRequest, CarResponse>
{
    public async Task<CarResponse> Handle(UpdateCarRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.Id, token);

        car.UpdateDetails(
            request.Brand,
            request.Model,
            request.Year,
            request.Mileage,
            request.FuelType,
            request.Description);

        await carRepository.UpdateAsync(car, token);

        return CarResponse.FromEntity(car);
    }
}