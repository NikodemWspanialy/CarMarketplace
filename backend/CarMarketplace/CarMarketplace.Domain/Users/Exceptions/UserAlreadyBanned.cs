using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class UserAlreadyBanned()
    : DomainException("User is already banned.");
