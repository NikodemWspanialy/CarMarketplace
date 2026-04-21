using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Cars.Exceptions;
using CarMarketplace.Domain.Common;

namespace CarMarketplace.Domain.Cars;

public class Car : IAggregateRoot
{
    public Guid Id { get; }

    public string Brand { get; private set; }

    public string Model { get; private set; }

    public int Year { get; private set; }

    public Money Price { get; private set; }

    public List<CarPriceHistory> PriceHistory { get; private set; }

    public int Mileage { get; private set; }

    public FuelType FuelType { get; private set; }

    public string? Description { get; private set; }

    public List<CarPhoto> Photos { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public Car(
        Guid id,
        string brand,
        string model,
        int year,
        Money price,
        int mileage,
        FuelType fuelType,
        string? description,
        List<CarPhoto> photos)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        CreatedAt = DateTime.UtcNow;
        PriceHistory =
        [
            new CarPriceHistory(id, price, CreatedAt)
        ];
        Mileage = mileage;
        FuelType = fuelType;
        Description = description;
        Photos = photos;
    }

    public void UpdateDetails(
        string brand,
        string model,
        int year,
        int mileage,
        FuelType fuelType,
        string? description)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Mileage = mileage;
        FuelType = fuelType;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Amount < 0)
            throw new InvalidCarPrice();

        if (Price.Amount == newPrice.Amount && Price.Currency == newPrice.Currency)
            throw new SamePriceAsCurrent();

        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;
        PriceHistory.Add(new CarPriceHistory(Id, newPrice, UpdatedAt.Value));
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new CarAlreadyDeleted();

        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}