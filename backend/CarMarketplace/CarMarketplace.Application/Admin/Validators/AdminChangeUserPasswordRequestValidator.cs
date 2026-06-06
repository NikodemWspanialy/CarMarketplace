using CarMarketplace.Application.Admin.Commands.AdminChangeUserPassword;
using CarMarketplace.Application.Users.Validators;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class AdminChangeUserPasswordRequestValidator : AbstractValidator<AdminChangeUserPasswordRequest>
{
    public AdminChangeUserPasswordRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.NewPassword).ValidPassword();
    }
}
