using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Queries.GetCar;

public record GetCarRequest(Guid Id) : IQuery<CarDetailsResponse>;