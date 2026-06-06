using CarMarketplace.Application.Admin.Commands.UpgradeToAdmin;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class UpgradeToAdminRequestValidator : AbstractValidator<UpgradeToAdminRequest>
{
    public UpgradeToAdminRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
