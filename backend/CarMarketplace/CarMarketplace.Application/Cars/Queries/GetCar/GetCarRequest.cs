using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Domain.Cars;

namespace CarMarketplace.Application.Cars.Queries.GetCar;

public record GetCarRequest(Guid Id) : IQuery<Car?>;