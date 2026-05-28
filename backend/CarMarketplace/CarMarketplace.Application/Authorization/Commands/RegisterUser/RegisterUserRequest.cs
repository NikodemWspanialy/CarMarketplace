using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Authorization.Commands.RegisterUser;

public record RegisterUserRequest(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;