using CarMarketplace.Domain.Common;

namespace CarMarketplace.Domain.Cars;

public class CarPriceHistory(Guid carId, Money price, DateTime changedAt)
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid CarId { get; private set; } = carId;

    public Money Price { get; private set; } = price;

    public DateTime ChangedAt { get; private set; } = changedAt;
}