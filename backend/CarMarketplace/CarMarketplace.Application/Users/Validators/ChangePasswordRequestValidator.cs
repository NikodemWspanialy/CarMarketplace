using CarMarketplace.Application.Users.Commands.ChangePassword;
using FluentValidation;

namespace CarMarketplace.Application.Users.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty();
        RuleFor(x => x.NewPassword).ValidPassword();
    }
}
