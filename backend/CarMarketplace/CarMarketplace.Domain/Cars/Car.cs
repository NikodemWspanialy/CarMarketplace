using CarMarketplace.Domain.Abstractions;

namespace CarMarketplace.Domain.Cars;

public class Car : IAggregateRoot
{
    public Guid Id { get; }

    public string Brand { get; private set; }

    public string Model { get; private set; }

    public int Year { get; private set; }

    public decimal Price { get; private set; }

    public int Mileage { get; private set; }

    public FuelType FuelType { get; private set; }

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public bool IsDeleted { get; private set; } = false;

    public Car(
        Guid id,
        string brand,
        string model,
        int year,
        decimal price,
        int mileage,
        FuelType fuelType,
        string? description)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        Mileage = mileage;
        FuelType = fuelType;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
}