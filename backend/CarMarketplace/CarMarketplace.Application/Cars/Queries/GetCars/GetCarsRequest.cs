using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Queries.GetCars;

public record GetCarsRequest(int PageNumber, int PageSize) : IQuery<IEnumerable<Car>>;