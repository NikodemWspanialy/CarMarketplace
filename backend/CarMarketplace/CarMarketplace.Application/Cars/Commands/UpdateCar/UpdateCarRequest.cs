using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Commands.UpdateCar;

public record UpdateCarRequest(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    int Mileage,
    FuelType FuelType,
    string? Description
) : ICommand<CarResponse>;