using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using FluentValidation;

namespace CarMarketplace.Application.Authorization.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
    }
}