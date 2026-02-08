using CarMarketplace.Application.Authorization.Exceptions;
using CarMarketplace.Application.Users.Repositories;

namespace CarMarketplace.Application.Authorization.Validators;

internal interface IRegisterUserValidator
{
    Task ValidateEmail(string email);
}

internal class RegisterUserValidator(IUserRepository userRepository) : IRegisterUserValidator
{
    public async Task ValidateEmail(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user is not null)
        {
            throw new EmailAlreadyTaken(email);
        }
    }
}