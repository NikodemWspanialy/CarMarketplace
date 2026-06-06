using CarMarketplace.Application.Listings.Commands.DeactivateListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class DeactivateListingRequestValidator : AbstractValidator<DeactivateListingRequest>
{
    public DeactivateListingRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
