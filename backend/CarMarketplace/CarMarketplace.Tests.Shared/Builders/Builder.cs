using Bogus;

namespace CarMarketplace.Tests.Shared.Builders;

public abstract class Builder<T>
{
    protected Faker Faker { get; } = new();

    public abstract T Build();
}
