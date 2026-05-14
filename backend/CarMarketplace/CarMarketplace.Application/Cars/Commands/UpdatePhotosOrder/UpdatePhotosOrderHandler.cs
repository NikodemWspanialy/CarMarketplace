using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;

internal class UpdatePhotosOrderHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarRepository carRepository) : IRequestHandler<UpdatePhotosOrderRequest, Unit>
{
    public async Task<Unit> Handle(UpdatePhotosOrderRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.CarId, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        car.UpdatePhotosOrder(
            request.Photos.Select(p => (p.Id, p.NewOrder)).ToList());

        await carRepository.UpdateAsync(car, token);

        return Unit.Value;
    }
}
