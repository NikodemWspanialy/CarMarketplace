namespace CarMarketplace.Domain.Cars;

public class CarPhoto(Guid carId, string url, bool isPrimary = false, int order = 0)
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid CarId { get; private set; } = carId;

    public string Url { get; private set; } = url;

    public bool IsPrimary { get; private set; } = isPrimary;

    public int Order { get; private set; } = order;

    public bool IsDeleted { get; set; } = false;

    public void SetAsPrimary()
    {
        IsPrimary = true;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}