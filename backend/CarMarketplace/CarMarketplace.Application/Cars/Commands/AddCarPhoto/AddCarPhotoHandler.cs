using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.AddCarPhoto;

internal class AddCarPhotoHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarPhotoFactory carPhotoFactory,
    ICarRepository carRepository) : IRequestHandler<AddCarPhotoRequest, CarPhotoResponse>
{
    public async Task<CarPhotoResponse> Handle(AddCarPhotoRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.CarId, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        var photo = carPhotoFactory.Create(request);
        car.AddPhoto(photo);
        await carRepository.UpdateAsync(car, token);

        return CarPhotoResponse.FromEntity(photo);
    }
}
