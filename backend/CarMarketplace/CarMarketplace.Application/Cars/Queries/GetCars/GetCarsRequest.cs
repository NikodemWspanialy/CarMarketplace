using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Common.DTOs;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

public record GetCarsRequest(int PageNumber, int PageSize) : IQuery<ListResponse<CarResponse>>;
