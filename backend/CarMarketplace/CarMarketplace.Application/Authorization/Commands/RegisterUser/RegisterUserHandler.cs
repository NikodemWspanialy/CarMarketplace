using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Authorization.Validators;
using CarMarketplace.Application.Users.Factories;
using CarMarketplace.Application.Users.Repositories;
using MediatR;

namespace CarMarketplace.Application.Authorization.Commands.RegisterUser;

internal class RegisterUserHandler(
    IUserRepository userRepository,
    IUserFactory userFactory,
    IRegisterUserValidator registerUserValidator,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterUserRequest, Guid>
{
    public async Task<Guid> Handle(RegisterUserRequest request, CancellationToken token)
    {
        await registerUserValidator.ValidateEmail(request.Email);

        var hash = passwordHasher.HashPassword(request.Password);
        var newUser = userFactory.Create(request, hash);

        await userRepository.AddUserAsync(newUser, token);

        return newUser.Id;
    }
}