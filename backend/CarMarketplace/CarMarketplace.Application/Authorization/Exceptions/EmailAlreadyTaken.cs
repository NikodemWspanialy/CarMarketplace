namespace CarMarketplace.Application.Authorization.Exceptions;

public class EmailAlreadyTaken(string email) : Exception($"Email '{email}' is already taken'");