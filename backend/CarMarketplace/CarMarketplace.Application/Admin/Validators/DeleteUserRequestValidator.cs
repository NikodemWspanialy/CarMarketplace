using CarMarketplace.Application.Admin.Commands.DeleteUser;
using FluentValidation;

namespace CarMarketplace.Application.Admin.Validators;

public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
