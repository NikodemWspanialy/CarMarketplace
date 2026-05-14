using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Application.Cars.Commands.AddCarPhotos;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Factories;

internal interface ICarPhotoFactory
{
    CarPhoto Create(AddCarPhotoRequest request);
    
    CarPhoto Create(Guid carId, AddCarPhotosItem item);
}

internal class CarPhotoFactory : ICarPhotoFactory
{
    public CarPhoto Create(AddCarPhotoRequest request) =>
        new(request.CarId, request.Url, request.IsPrimary, request.Order);

    public CarPhoto Create(Guid carId, AddCarPhotosItem item) =>
        new(carId, item.Url, item.IsPrimary, item.Order);
}
