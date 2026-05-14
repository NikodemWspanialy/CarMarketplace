using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Searchers;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdateCarPrice;

internal class UpdateCarPriceHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarRepository carRepository,
    IMoneyFactory moneyFactory)
    : IRequestHandler<UpdateCarPriceRequest, CarDetailsResponse>
{
    public async Task<CarDetailsResponse> Handle(UpdateCarPriceRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.Id, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        var newPrice = moneyFactory.Create(request.PriceAmount, request.PriceCurrency, car.Price);
        car.UpdatePrice(newPrice);

        await carRepository.UpdateAsync(car, token);

        return CarDetailsResponse.FromEntity(car);
    }
}