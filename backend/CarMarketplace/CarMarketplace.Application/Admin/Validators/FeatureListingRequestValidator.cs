using CarMarketplace.Application.Admin.Commands.FeatureListing;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class FeatureListingRequestValidator : AbstractValidator<FeatureListingRequest>
{
    public FeatureListingRequestValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.Until).GreaterThan(DateTime.UtcNow);
    }
}
