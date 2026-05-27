using CarMarketplace.Application.Authorization.Commands.ForgotPassword;
using FluentValidation;

namespace CarMarketplace.Application.Authorization.Validators;

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
