using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCar;

public record DeleteCarRequest(Guid Id) : ICommand<Unit>;
