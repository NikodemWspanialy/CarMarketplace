using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.Application.Authorization.Exceptions;

public class UserIsBanned() : DomainException("User account is banned.");
