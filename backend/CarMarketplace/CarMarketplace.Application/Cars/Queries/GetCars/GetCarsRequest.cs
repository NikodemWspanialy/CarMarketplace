using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

public record GetCarsRequest(int PageNumber, int PageSize) : IQuery<CarListResponse>;