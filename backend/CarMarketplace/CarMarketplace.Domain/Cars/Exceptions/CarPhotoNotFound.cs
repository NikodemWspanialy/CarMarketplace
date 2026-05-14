using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Cars.Exceptions;

public class CarPhotoNotFound(Guid photoId)
    : DomainException($"Photo with id '{photoId}' was not found.");
