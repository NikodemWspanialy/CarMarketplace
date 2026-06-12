using CarMarketplace.Application.Contacts.Commands.DeleteContact;
using FluentValidation;

namespace CarMarketplace.Application.Contacts.Validators;

public class DeleteContactRequestValidator : AbstractValidator<DeleteContactRequest>
{
    public DeleteContactRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
