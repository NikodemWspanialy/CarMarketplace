using CarMarketplace.Application.Admin.Queries.GetBanHistory;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class GetBanHistoryRequestValidator : AbstractValidator<GetBanHistoryRequest>
{
    public GetBanHistoryRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
