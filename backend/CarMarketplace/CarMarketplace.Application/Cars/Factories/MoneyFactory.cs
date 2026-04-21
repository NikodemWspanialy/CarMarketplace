using CarMarketplace.Domain.Common;

namespace CarMarketplace.Application.Cars.Factories;

internal interface IMoneyFactory
{
    Money Create(decimal amount, string? currency, Money currentPrice);
}

internal class MoneyFactory : IMoneyFactory
{
    public Money Create(decimal amount, string? currency, Money currentPrice) =>
        new(amount, currency ?? currentPrice.Currency);
}