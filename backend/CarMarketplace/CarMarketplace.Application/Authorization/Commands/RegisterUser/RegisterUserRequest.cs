using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Authorization.Commands.RegisterUser;

public record class RegisterUserRequest(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;