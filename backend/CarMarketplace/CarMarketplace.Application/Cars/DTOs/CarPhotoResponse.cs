using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.DTOs;

public record CarPhotoResponse(
    Guid Id,
    string Url,
    bool IsPrimary,
    int Order)
{
    public static CarPhotoResponse FromEntity(CarPhoto photo) =>
        new(photo.Id, photo.Url, photo.IsPrimary, photo.Order);
}
