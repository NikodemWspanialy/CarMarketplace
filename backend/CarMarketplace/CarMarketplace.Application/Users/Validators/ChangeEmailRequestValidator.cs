using CarMarketplace.Application.Users.Commands.ChangeEmail;
using FluentValidation;

namespace CarMarketplace.Application.Users.Validators;

public class ChangeEmailRequestValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailRequestValidator()
    {
        RuleFor(x => x.NewEmail)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();
    }
}
