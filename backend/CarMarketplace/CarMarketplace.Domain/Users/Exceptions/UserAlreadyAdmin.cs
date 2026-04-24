using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class UserAlreadyAdmin()
    : DomainException("User is already an admin.");
