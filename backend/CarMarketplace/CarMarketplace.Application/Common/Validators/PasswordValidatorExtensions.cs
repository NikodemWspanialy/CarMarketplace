using FluentValidation;

namespace CarMarketplace.Application.Common.Validators;

public static class PasswordValidatorExtensions
{
    public static void ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.NotEmpty().MinimumLength(6);
}
