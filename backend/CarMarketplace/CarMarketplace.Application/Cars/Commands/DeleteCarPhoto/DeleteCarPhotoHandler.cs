using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;

internal class DeleteCarPhotoHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarRepository carRepository) : IRequestHandler<DeleteCarPhotoRequest, Unit>
{
    public async Task<Unit> Handle(DeleteCarPhotoRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.CarId, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        car.DeletePhoto(request.PhotoId);
        await carRepository.UpdateAsync(car, token);

        return Unit.Value;
    }
}
