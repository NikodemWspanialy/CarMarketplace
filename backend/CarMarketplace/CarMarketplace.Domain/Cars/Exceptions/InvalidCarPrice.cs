using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Cars.Exceptions;

public class InvalidCarPrice()
    : DomainException("Price must be greater than or equal to zero.");