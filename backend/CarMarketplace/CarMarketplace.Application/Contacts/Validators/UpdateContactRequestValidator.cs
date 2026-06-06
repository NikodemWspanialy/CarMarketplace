using CarMarketplace.Application.Contacts.Commands.UpdateContact;
using FluentValidation;

namespace CarMarketplace.Application.Contacts.Validators;

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        this.ApplyContactDetailsRules(
            x => x.Type,
            x => x.PhoneNumber,
            x => x.EmailAddress,
            x => x.Username,
            x => x.Label,
            x => x.CountryCode);
    }
}
