using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Cars.Exceptions;

public class CarNotFound(Guid id)
    : DomainException($"Car with id '{id}' was not found.");