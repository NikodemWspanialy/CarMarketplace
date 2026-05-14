using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.DTOs;

public record CarResponse(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    decimal PriceAmount,
    string PriceCurrency,
    int Mileage,
    FuelType FuelType,
    string? PhotoUrl)
{
    public static CarResponse FromEntity(Car car) =>
        new(car.Id,
            car.Brand,
            car.Model,
            car.Year,
            car.Price.Amount,
            car.Price.Currency,
            car.Mileage,
            car.FuelType,
            car.Photos.FirstOrDefault(p => p is { IsPrimary: true, IsDeleted: false })?.Url);
}
