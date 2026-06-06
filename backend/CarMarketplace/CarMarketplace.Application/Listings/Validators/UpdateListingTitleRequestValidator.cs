using CarMarketplace.Application.Listings.Commands.UpdateListingTitle;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class UpdateListingTitleRequestValidator : AbstractValidator<UpdateListingTitleRequest>
{
    public UpdateListingTitleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
    }
}
