using System.Linq.Expressions;
using CarMarketplace.Domain.Contacts;
using FluentValidation;

namespace CarMarketplace.Application.Contacts.Validators;

public static class ContactValidationRules
{
    public static void ApplyContactDetailsRules<T>(
        this AbstractValidator<T> validator,
        Expression<Func<T, ContactType>> typeExpr,
        Expression<Func<T, string?>> phoneNumberExpr,
        Expression<Func<T, string?>> emailAddressExpr,
        Expression<Func<T, string?>> usernameExpr,
        Expression<Func<T, string?>> labelExpr,
        Expression<Func<T, string?>> countryCodeExpr)
    {
        validator.RuleFor(typeExpr).IsInEnum();

        validator.RuleFor(phoneNumberExpr)
            .NotEmpty()
            .When(x => typeExpr.Compile()(x) is ContactType.Phone or ContactType.WhatsApp)
            .WithMessage("PhoneNumber is required for this contact type.");

        validator.RuleFor(phoneNumberExpr)
            .MaximumLength(20)
            .When(x => phoneNumberExpr.Compile()(x) is not null);

        validator.RuleFor(emailAddressExpr)
            .NotEmpty()
            .EmailAddress()
            .When(x => typeExpr.Compile()(x) == ContactType.Email)
            .WithMessage("A valid EmailAddress is required for Email contact type.");

        validator.RuleFor(emailAddressExpr)
            .MaximumLength(256)
            .When(x => emailAddressExpr.Compile()(x) is not null);

        validator.RuleFor(usernameExpr)
            .NotEmpty()
            .When(x => typeExpr.Compile()(x) == ContactType.Telegram)
            .WithMessage("Username is required for Telegram contact type.");

        validator.RuleFor(usernameExpr)
            .MaximumLength(100)
            .When(x => usernameExpr.Compile()(x) is not null);

        validator.RuleFor(labelExpr).MaximumLength(100);
        validator.RuleFor(countryCodeExpr).MaximumLength(5);
    }
}
