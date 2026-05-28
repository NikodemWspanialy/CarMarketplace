using Bogus;
using CarMarketplace.Application.Cars.Commands.CreateCar;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Tests.Shared.Builders.Car;

public class CreateCarRequestBuilder : Builder<CreateCarRequest>
{
    private string _brand;
    private string _model;
    private int _year;
    private decimal _priceAmount;
    private string _priceCurrency;
    private int _mileage;
    private FuelType _fuelType;
    private string? _description;

    public CreateCarRequestBuilder()
    {
        _brand = Faker.Vehicle.Manufacturer();
        _model = Faker.Vehicle.Model();
        _year = Faker.Date.Past(5).Year;
        _priceAmount = Faker.Random.Decimal(10000, 500000);
        _priceCurrency = "PLN";
        _mileage = Faker.Random.Int(0, 300000);
        _fuelType = Faker.PickRandom<FuelType>();
        _description = Faker.Lorem.Sentence();
    }

    public CreateCarRequestBuilder WithBrand(string brand) { _brand = brand; return this; }
    public CreateCarRequestBuilder WithModel(string model) { _model = model; return this; }
    public CreateCarRequestBuilder WithYear(int year) { _year = year; return this; }
    public CreateCarRequestBuilder WithPrice(decimal amount, string currency = "PLN") { _priceAmount = amount; _priceCurrency = currency; return this; }
    public CreateCarRequestBuilder WithMileage(int mileage) { _mileage = mileage; return this; }
    public CreateCarRequestBuilder WithFuelType(FuelType fuelType) { _fuelType = fuelType; return this; }
    public CreateCarRequestBuilder WithDescription(string? description) { _description = description; return this; }

    public override CreateCarRequest Build() =>
        new(_brand, _model, _year, _priceAmount, _priceCurrency, _mileage, _fuelType, _description);
}
