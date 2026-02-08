using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.RegisterUser;

public record class RegisterUserRequest(string Email, string Password, string FirstName, string LastName)
    : IRequest<Guid>;