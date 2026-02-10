using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Commands.CreateCar;

public record CreateCarRequest(
    string Brand,
    string Model,
    int Year,
    decimal Price,
    int Mileage,
    FuelType FuelType,
    string? Description) 
    : ICommand<Guid>;
