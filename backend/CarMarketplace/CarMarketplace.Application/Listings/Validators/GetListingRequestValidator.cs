using CarMarketplace.Application.Listings.Queries.GetListing;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class GetListingRequestValidator : AbstractValidator<GetListingRequest>
{
    public GetListingRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
