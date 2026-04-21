using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Exceptions;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdateCarPrice;

internal class UpdateCarPriceHandler(
    ICarRepository carRepository,
    IMoneyFactory moneyFactory)
    : IRequestHandler<UpdateCarPriceRequest, CarResponse>
{
    public async Task<CarResponse> Handle(UpdateCarPriceRequest request, CancellationToken token)
    {
        var car = await carRepository.GetByIdAsync(request.Id, token)
                  ?? throw new CarNotFoundException(request.Id);

        var newPrice = moneyFactory.Create(request.PriceAmount, request.PriceCurrency, car.Price);
        car.UpdatePrice(newPrice);

        await carRepository.UpdateAsync(car, token);

        return CarResponse.FromEntity(car);
    }
}