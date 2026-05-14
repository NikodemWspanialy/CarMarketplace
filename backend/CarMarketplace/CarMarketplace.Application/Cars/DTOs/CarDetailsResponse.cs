using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.DTOs;

public record CarDetailsResponse(
    Guid Id,
    Guid SellerId,
    string Brand,
    string Model,
    int Year,
    decimal PriceAmount,
    string PriceCurrency,
    int Mileage,
    FuelType FuelType,
    string? Description,
    IReadOnlyList<CarPhotoResponse> Photos,
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static CarDetailsResponse FromEntity(Car car) =>
        new(car.Id,
            car.SellerId,
            car.Brand,
            car.Model,
            car.Year,
            car.Price.Amount,
            car.Price.Currency,
            car.Mileage,
            car.FuelType,
            car.Description,
            car.Photos
                .Where(p => !p.IsDeleted)
                .OrderBy(p => p.Order)
                .Select(CarPhotoResponse.FromEntity)
                .ToList(),
            car.CreatedAt,
            car.UpdatedAt);
}
