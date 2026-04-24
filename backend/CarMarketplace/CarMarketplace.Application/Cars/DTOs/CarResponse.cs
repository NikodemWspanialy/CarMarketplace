using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.DTOs;

public record CarResponse(
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
    DateTime CreatedAt,
    DateTime? UpdatedAt)
{
    public static CarResponse FromEntity(Car car) =>
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
            car.CreatedAt,
            car.UpdatedAt);
}
