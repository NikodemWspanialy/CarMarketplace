using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdateCarPrice;

internal class UpdateCarPriceHandler(
    ICarSearcher carSearcher,
    ICarRepository carRepository,
    IMoneyFactory moneyFactory)
    : IRequestHandler<UpdateCarPriceRequest, CarResponse>
{
    public async Task<CarResponse> Handle(UpdateCarPriceRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.Id, token);

        var newPrice = moneyFactory.Create(request.PriceAmount, request.PriceCurrency, car.Price);
        car.UpdatePrice(newPrice);

        await carRepository.UpdateAsync(car, token);

        return CarResponse.FromEntity(car);
    }
}