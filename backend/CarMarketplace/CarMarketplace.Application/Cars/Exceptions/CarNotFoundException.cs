using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Cars.Exceptions;

public class CarNotFoundException(Guid id)
    : DomainException($"Car with id '{id}' was not found.");
