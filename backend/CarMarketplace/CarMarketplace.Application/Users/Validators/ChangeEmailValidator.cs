using CarMarketplace.Application.Authorization.Exceptions;
using CarMarketplace.Application.Users.Repositories;

namespace CarMarketplace.Application.Users.Validators;

internal interface IChangeEmailValidator
{
    Task ValidateEmailNotTaken(string email, CancellationToken token = default);
}

internal class ChangeEmailValidator(IUserRepository userRepository) : IChangeEmailValidator
{
    public async Task ValidateEmailNotTaken(string email, CancellationToken token = default)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(email, token);
        if (existingUser is not null)
            throw new EmailAlreadyTaken(email);
    }
}
