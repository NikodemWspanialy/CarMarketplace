using CarMarketplace.Application.Listings.Commands.RevealListingContacts;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class RevealListingContactsRequestValidator : AbstractValidator<RevealListingContactsRequest>
{
    public RevealListingContactsRequestValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
    }
}
