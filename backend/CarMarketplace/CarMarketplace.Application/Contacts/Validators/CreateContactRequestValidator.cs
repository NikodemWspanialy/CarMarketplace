using CarMarketplace.Application.Contacts.Commands.CreateContact;
using FluentValidation;

namespace CarMarketplace.Application.Contacts.Validators;

public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactRequestValidator()
    {
        this.ApplyContactDetailsRules(
            x => x.Type,
            x => x.PhoneNumber,
            x => x.EmailAddress,
            x => x.Username,
            x => x.Label,
            x => x.CountryCode);
    }
}
