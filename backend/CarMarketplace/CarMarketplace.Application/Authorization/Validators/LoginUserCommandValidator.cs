using CarMarketplace.Application.Authorization.Queries.LoginUser;
using FluentValidation;

namespace CarMarketplace.Application.Authorization.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Email is required");
    }
}