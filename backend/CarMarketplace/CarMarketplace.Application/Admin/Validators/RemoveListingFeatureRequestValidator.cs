using CarMarketplace.Application.Admin.Commands.RemoveListingFeature;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class RemoveListingFeatureRequestValidator : AbstractValidator<RemoveListingFeatureRequest>
{
    public RemoveListingFeatureRequestValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
    }
}
