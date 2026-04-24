using CarMarketplace.Application.Users.Commands.UpdateUserProfile;
using FluentValidation;

namespace CarMarketplace.Application.Users.Validators;

public class UpdateUserProfileRequestValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
    }
}
