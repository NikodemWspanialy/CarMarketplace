using CarMarketplace.Application.Admin.Commands.DowngradeToUser;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class DowngradeToUserRequestValidator : AbstractValidator<DowngradeToUserRequest>
{
    public DowngradeToUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
