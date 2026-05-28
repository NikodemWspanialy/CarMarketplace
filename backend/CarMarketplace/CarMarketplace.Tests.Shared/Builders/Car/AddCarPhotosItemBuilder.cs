using CarMarketplace.Application.Cars.Commands.AddCarPhotos;

namespace CarMarketplace.Tests.Shared.Builders.Car;

public class AddCarPhotosItemBuilder : Builder<AddCarPhotosItem>
{
    private string _url;
    private int _order;
    private bool _isPrimary;

    public AddCarPhotosItemBuilder()
    {
        _url = Faker.Internet.Url();
        _order = Faker.Random.Int(1, 20);
        _isPrimary = false;
    }

    public AddCarPhotosItemBuilder WithUrl(string url) { _url = url; return this; }
    public AddCarPhotosItemBuilder WithOrder(int order) { _order = order; return this; }
    public AddCarPhotosItemBuilder AsPrimary() { _isPrimary = true; return this; }

    public override AddCarPhotosItem Build() => new(_url, _order, _isPrimary);
}
