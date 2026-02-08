using CarMarketplace.Application.Authorization.DTOs;
using CarMarketplace.Application.Authorization.Exceptions;
using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Users.Repositories;
using MediatR;

namespace CarMarketplace.Application.Authorization.Queries.LoginUser;

internal class LoginUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider)
    : IRequestHandler<LoginUserQuery, AuthResponse?>
{
    public async Task<AuthResponse?> Handle(LoginUserQuery request, CancellationToken token)
    {
        var user = await userRepository.GetUserByEmailAsync(request.Email, token);
        if (user is null)
        {
            throw new InvalidCredentials();
        }

        if (!passwordHasher.VerifyHashedPassword(request.Password, user.PasswordHash))
        {
            throw new InvalidCredentials();
        }

        var accessToken = jwtProvider.Generate(user);

        return new AuthResponse(accessToken);
    }
}