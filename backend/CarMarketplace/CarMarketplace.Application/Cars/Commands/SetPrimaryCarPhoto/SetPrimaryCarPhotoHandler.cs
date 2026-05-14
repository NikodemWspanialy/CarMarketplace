using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;

internal class SetPrimaryCarPhotoHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarRepository carRepository) : IRequestHandler<SetPrimaryCarPhotoRequest, CarPhotoResponse>
{
    public async Task<CarPhotoResponse> Handle(SetPrimaryCarPhotoRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.CarId, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        var photo = car.SetPrimaryPhoto(request.PhotoId);
        await carRepository.UpdateAsync(car, token);

        return CarPhotoResponse.FromEntity(photo);
    }
}
