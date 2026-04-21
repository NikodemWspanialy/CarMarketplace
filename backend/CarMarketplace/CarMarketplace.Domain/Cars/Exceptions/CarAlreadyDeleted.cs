using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Cars.Exceptions;

public class CarAlreadyDeleted()
    : DomainException("Car is already deleted.");