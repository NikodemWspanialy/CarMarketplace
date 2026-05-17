using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Domain.Users.Exceptions;

public class InvalidResetToken()
    : DomainException("Reset token is invalid or expired.");
