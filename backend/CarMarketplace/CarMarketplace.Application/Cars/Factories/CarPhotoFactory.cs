using CarMarketplace.Application.Cars.Commands.AddCarPhoto;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Factories;

internal interface ICarPhotoFactory
{
    CarPhoto Create(AddCarPhotoRequest request);
}

internal class CarPhotoFactory : ICarPhotoFactory
{
    public CarPhoto Create(AddCarPhotoRequest request) =>
        new(request.CarId, request.Url, request.IsPrimary, request.Order);
}
