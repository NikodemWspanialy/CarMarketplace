namespace CarMarketplace.Domain.Cars;

public class CarPhoto
{
    public Guid Id { get; private set; }

    public Guid CarId { get; private set; }

    public string Url { get; private set; }

    public bool IsPrimary { get; private set; }

    public int Order { get; private set; }

    public bool IsDeleted { get; private set; }

    // EF Core
    private CarPhoto() { }

    public CarPhoto(Guid carId, string url, bool isPrimary = false, int order = 0)
    {
        Id = Guid.NewGuid();
        CarId = carId;
        Url = url;
        IsPrimary = isPrimary;
        Order = order;
    }

    public void SetAsPrimary() => IsPrimary = true;

    public void Delete() => IsDeleted = true;
}
