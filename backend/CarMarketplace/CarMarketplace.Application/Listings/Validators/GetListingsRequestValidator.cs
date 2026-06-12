using CarMarketplace.Application.Listings.Queries.GetListings;
using FluentValidation;

namespace CarMarketplace.Application.Listings.Validators;

public class GetListingsRequestValidator : AbstractValidator<GetListingsRequest>
{
    public GetListingsRequestValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}
