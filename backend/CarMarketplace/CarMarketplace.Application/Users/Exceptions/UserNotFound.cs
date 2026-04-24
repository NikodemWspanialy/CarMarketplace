using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Users.Exceptions;

public class UserNotFound(Guid id)
    : DomainException($"User with id '{id}' was not found.");
