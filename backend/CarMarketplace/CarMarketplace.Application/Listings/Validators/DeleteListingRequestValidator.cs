using CarMarketplace.Application.Listings.Commands.DeleteListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class DeleteListingRequestValidator : AbstractValidator<DeleteListingRequest>
{
    public DeleteListingRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
