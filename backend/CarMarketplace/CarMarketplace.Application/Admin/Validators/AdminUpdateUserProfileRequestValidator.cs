using CarMarketplace.Application.Admin.Commands.AdminUpdateUserProfile;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class AdminUpdateUserProfileRequestValidator : AbstractValidator<AdminUpdateUserProfileRequest>
{
    public AdminUpdateUserProfileRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
