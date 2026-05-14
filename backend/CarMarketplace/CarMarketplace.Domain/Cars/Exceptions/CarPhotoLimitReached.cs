using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Cars.Exceptions;

public class CarPhotoLimitReached()
    : DomainException("Maximum of 20 photos per car has been reached.");
