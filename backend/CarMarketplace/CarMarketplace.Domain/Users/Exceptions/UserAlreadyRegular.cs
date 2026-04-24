using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class UserAlreadyRegular()
    : DomainException("User is already a regular user.");
