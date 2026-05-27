using CarMarketplace.Application.Authorization.Commands.ResetPassword;
using FluentValidation;

namespace CarMarketplace.Application.Authorization.Validators;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}
