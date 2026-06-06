using CarMarketplace.Application.Admin.Queries.GetUsers;
using CarMarketplace.Application.Common.Validators;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class GetUsersRequestValidator : AbstractValidator<GetUsersRequest>
{
    public GetUsersRequestValidator()
    {
        this.ValidPaging(x => x.PageNumber, x => x.PageSize);
    }
}
