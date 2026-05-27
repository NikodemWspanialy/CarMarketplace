using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class UserNotBanned()
    : DomainException("User is not banned.");
