using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Repositories;
using CarMarketplace.Application.Cars.Searchers;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.AddCarPhotos;

internal class AddCarPhotosHandler(
    ICarSearcher carSearcher,
    ICarSellerGuard carSellerGuard,
    ICarPhotoFactory carPhotoFactory,
    ICarRepository carRepository) : IRequestHandler<AddCarPhotosRequest, IReadOnlyList<CarPhotoResponse>>
{
    public async Task<IReadOnlyList<CarPhotoResponse>> Handle(AddCarPhotosRequest request, CancellationToken token)
    {
        var car = await carSearcher.FindByIdAsync(request.CarId, token);
        carSellerGuard.EnsureCanMutate(car.SellerId);

        var photos = request.Photos
            .Select(p => carPhotoFactory.Create(request.CarId, p))
            .ToList();

        car.AddPhotos(photos);
        await carRepository.UpdateAsync(car, token);

        return photos.Select(CarPhotoResponse.FromEntity).ToList();
    }
}
