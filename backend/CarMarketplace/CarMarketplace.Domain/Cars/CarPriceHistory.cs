using CarMarketplace.Domain.Common;

namespace CarMarketplace.Domain.Cars;

public class CarPriceHistory
{
    public Guid Id { get; private set; }

    public Guid CarId { get; private set; }

    public Money Price { get; private set; }

    public DateTime ChangedAt { get; private set; }

    // EF Core
    private CarPriceHistory() { }

    public CarPriceHistory(Guid carId, Money price, DateTime changedAt)
    {
        Id = Guid.NewGuid();
        CarId = carId;
        Price = price;
        ChangedAt = changedAt;
    }
}
