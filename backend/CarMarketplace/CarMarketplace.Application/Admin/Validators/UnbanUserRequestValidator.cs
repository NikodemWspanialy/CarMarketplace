using CarMarketplace.Application.Admin.Commands.UnbanUser;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class UnbanUserRequestValidator : AbstractValidator<UnbanUserRequest>
{
    public UnbanUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Reason)
            .MaximumLength(500)
            .When(x => x.Reason is not null);
    }
}
