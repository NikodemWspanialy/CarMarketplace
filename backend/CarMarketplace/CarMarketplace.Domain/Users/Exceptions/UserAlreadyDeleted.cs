using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class UserAlreadyDeleted()
    : DomainException("User is already deleted.");
