using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Commands.DeleteCar;

public record DeleteCarRequest(Guid Id) : ICommand<bool>;
