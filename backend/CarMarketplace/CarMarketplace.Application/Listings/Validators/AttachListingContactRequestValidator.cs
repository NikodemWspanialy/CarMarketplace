using CarMarketplace.Application.Listings.Commands.AttachListingContact;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class AttachListingContactRequestValidator : AbstractValidator<AttachListingContactRequest>
{
    public AttachListingContactRequestValidator()
    {
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.ContactId).NotEmpty();
    }
}
