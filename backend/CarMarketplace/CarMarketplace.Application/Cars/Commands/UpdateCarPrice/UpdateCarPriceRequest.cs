using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Commands.UpdateCarPrice;

public record UpdateCarPriceRequest(
    Guid Id,
    decimal PriceAmount,
    string? PriceCurrency = null) : ICommand<CarResponse>;