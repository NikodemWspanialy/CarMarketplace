using CarMarketplace.Application.Listings.Commands.DetachListingContact;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class DetachListingContactRequestValidator : AbstractValidator<DetachListingContactRequest>
{
    public DetachListingContactRequestValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.ContactId).NotEmpty();
    }
}
