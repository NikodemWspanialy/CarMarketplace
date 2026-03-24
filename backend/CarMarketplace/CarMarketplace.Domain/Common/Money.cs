using CarMarketplace.Domain.Common.Exceptions;

namespace CarMarketplace.Domain.Common;

public record Money
{
    public decimal Amount { get; private set; }

    public string Currency { get; private set; }

    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrEmpty(currency))
        {
            throw new EmptyOrNullCurrency();
        }

        Currency = currency;
        Amount = amount;
    }
}