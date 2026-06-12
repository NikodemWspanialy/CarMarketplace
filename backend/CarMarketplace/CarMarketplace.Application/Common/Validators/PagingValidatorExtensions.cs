using System.Linq.Expressions;
using FluentValidation;

namespace CarMarketplace.Application.Common.Validators;

public static class PagingValidatorExtensions
{
    public static void ValidPaging<T>(
        this AbstractValidator<T> validator,
        Expression<Func<T, int>> pageNumberExpression,
        Expression<Func<T, int>> pageSizeExpression)
    {
        validator.RuleFor(pageNumberExpression).GreaterThanOrEqualTo(1);
        validator.RuleFor(pageSizeExpression).InclusiveBetween(1, 100);
    }
}
