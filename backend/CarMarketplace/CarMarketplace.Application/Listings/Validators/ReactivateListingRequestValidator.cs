using CarMarketplace.Application.Listings.Commands.ReactivateListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class ReactivateListingRequestValidator : AbstractValidator<ReactivateListingRequest>
{
    public ReactivateListingRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
