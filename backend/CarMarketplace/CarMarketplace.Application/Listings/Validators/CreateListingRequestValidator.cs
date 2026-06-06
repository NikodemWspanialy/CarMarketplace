using CarMarketplace.Application.Listings.Commands.CreateListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class CreateListingRequestValidator : AbstractValidator<CreateListingRequest>
{
    public CreateListingRequestValidator()
    {
        RuleFor(x => x.CarId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ContactIds).NotEmpty().WithMessage("At least one contact is required.");
        RuleFor(x => x.ContactIds)
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("ContactIds must be unique.");
    }
}
