using CarMarketplace.Application.Users.Queries.GetUserById;
using FluentValidation;

namespace CarMarketplace.Application.Users.Validators;

public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
