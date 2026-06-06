using CarMarketplace.Application.Contacts.Commands.CreateContact;
using CarMarketplace.Domain.Contacts;
using FluentValidation;

namespace CarMarketplace.Application.Contacts.Validators;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactRequestValidator()
    {
        RuleFor(x => x.Type).IsInEnum();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .When(x => x.Type is ContactType.Phone or ContactType.WhatsApp)
            .WithMessage("PhoneNumber is required for this contact type.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .When(x => x.PhoneNumber is not null);

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress()
            .When(x => x.Type == ContactType.Email)
            .WithMessage("A valid EmailAddress is required for Email contact type.");

        RuleFor(x => x.EmailAddress)
            .MaximumLength(256)
            .When(x => x.EmailAddress is not null);

        RuleFor(x => x.Username)
            .NotEmpty()
            .When(x => x.Type == ContactType.Telegram)
            .WithMessage("Username is required for Telegram contact type.");

        RuleFor(x => x.Username)
            .MaximumLength(100)
            .When(x => x.Username is not null);

        RuleFor(x => x.Label).MaximumLength(100);
        RuleFor(x => x.CountryCode).MaximumLength(5);
    }
}
