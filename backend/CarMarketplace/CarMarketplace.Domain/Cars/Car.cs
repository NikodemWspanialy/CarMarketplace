using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Cars.Exceptions;
using CarMarketplace.Domain.Common;

namespace CarMarketplace.Domain.Cars;

public class Car : IAggregateRoot
{
    public Guid Id { get; private set; }

    public Guid SellerId { get; private set; }

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

    // EF Core
    private Car() { }

    public Car(
        Guid id,
        Guid sellerId,
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
        SellerId = sellerId;
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

    public void AddPhoto(CarPhoto photo)
    {
        if (IsDeleted)
            throw new CarAlreadyDeleted();

        var nonDeletedPhotos = Photos.Where(p => !p.IsDeleted).ToList();

        if (nonDeletedPhotos.Count >= 20)
            throw new CarPhotoLimitReached();

        if (photo.IsPrimary)
            foreach (var existing in nonDeletedPhotos.Where(p => p.IsPrimary))
                existing.UnsetPrimary();

        Photos.Add(photo);
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddPhotos(List<CarPhoto> photos)
    {
        if (IsDeleted)
            throw new CarAlreadyDeleted();

        var nonDeletedCount = Photos.Count(p => !p.IsDeleted);

        if (nonDeletedCount + photos.Count > 20)
            throw new CarPhotoLimitReached();

        var hasPrimaryInBatch = photos.Any(p => p.IsPrimary);
        if (hasPrimaryInBatch)
            foreach (var existing in Photos.Where(p => p is { IsDeleted: false, IsPrimary: true }))
                existing.UnsetPrimary();

        foreach (var photo in photos)
            Photos.Add(photo);

        UpdatedAt = DateTime.UtcNow;
    }

    public void DeletePhoto(Guid photoId)
    {
        var photo = Photos.FirstOrDefault(p => p.Id == photoId && !p.IsDeleted)
            ?? throw new CarPhotoNotFound(photoId);

        photo.Delete();
        UpdatedAt = DateTime.UtcNow;
    }

    public CarPhoto SetPrimaryPhoto(Guid photoId)
    {
        var photo = Photos.FirstOrDefault(p => p.Id == photoId && !p.IsDeleted)
            ?? throw new CarPhotoNotFound(photoId);

        if (photo.IsPrimary)
            return photo;

        foreach (var p in Photos.Where(p => p is { IsDeleted: false, IsPrimary: true }))
            p.UnsetPrimary();

        photo.SetAsPrimary();
        UpdatedAt = DateTime.UtcNow;

        return photo;
    }

    public void UpdatePhotosOrder(List<(Guid PhotoId, int NewOrder)> updates)
    {
        foreach (var (photoId, newOrder) in updates)
        {
            var photo = Photos.FirstOrDefault(p => p.Id == photoId && !p.IsDeleted)
                ?? throw new CarPhotoNotFound(photoId);

            photo.UpdateOrder(newOrder);
        }

        UpdatedAt = DateTime.UtcNow;
    }
}