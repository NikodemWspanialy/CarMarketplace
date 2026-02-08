using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Users.Factories;

internal interface IUserFactory
{
    User Create(RegisterUserRequest request, string hash);
}

internal class UserFactory : IUserFactory
{
    public User Create(RegisterUserRequest request, string hash) =>
        new User(
            email: request.Email,
            passwordHash: hash,
            firstName: request.FirstName,
            lastName: request.LastName,
            createdAt: DateTime.UtcNow);
}