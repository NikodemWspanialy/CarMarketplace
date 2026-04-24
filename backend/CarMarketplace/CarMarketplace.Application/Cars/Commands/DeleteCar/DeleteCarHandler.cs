using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCar;

internal class DeleteCarHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarRepository carRepository)
    : IRequestHandler<DeleteCarRequest, Unit>
{
    public async Task<Unit> Handle(DeleteCarRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.Id, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        car.Delete();

        await carRepository.UpdateAsync(car, token);

        return Unit.Value;
    }
}