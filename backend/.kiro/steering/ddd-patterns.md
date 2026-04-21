---
inclusion: fileMatch
fileMatchPattern: "**/Domain/**,**/Application/**/Commands/**,**/Application/**/Queries/**"
---

# DDD Patterns in CarMarketplace

## Core Principles

### Layer Separation (strict rules)
- Domain MUST NOT depend on EF Core, Infrastructure, Application or API
- Application MUST NOT contain business logic — only orchestration
- Infrastructure MUST NOT contain domain rules
- Domain MUST be persistence-agnostic — no EF attributes, no DbContext references

### Avoid Anemic Domain Model
- No public setters on entities — state changes only through domain methods
- If logic belongs to an entity, it MUST live in the entity — not in a service or handler
- Entities are not data bags — they encapsulate behavior

## Aggregate Root

- Each aggregate implements `IAggregateRoot`
- The aggregate is the only entry point for modifying its child entities
- Aggregates reference other aggregates ONLY by Id (never by object reference)
- Keep aggregates small — they define consistency boundaries
- If two things don't need to be consistent in the same transaction, they belong to separate aggregates

### When to create a new aggregate
- It has its own identity and lifecycle
- It needs independent consistency guarantees
- It can be loaded/saved independently

### Creating aggregates
- Prefer static factory methods over constructors for complex creation logic
- Simple aggregates can use constructors directly
- Validate all invariants at creation time

```csharp
// Factory method pattern
public static Car Create(string brand, string model, int year, Money price, int mileage, FuelType fuelType, string? description)
{
    // validate invariants
    if (price.Amount <= 0) throw new InvalidCarPrice();

    return new Car(Guid.NewGuid(), brand, model, year, price, mileage, fuelType, description);
}
```

## Invariants vs Validation

### Domain invariants (Domain layer)
- Business rules that MUST always be true
- Enforced in entity constructors and domain methods
- Throw custom domain exceptions on violation
- Examples: price must be > 0, max 10 photos per car

### Input validation (Application layer)
- Technical validation of incoming data (format, required fields, length)
- Handled by FluentValidation validators
- Examples: email format, string max length, required fields

These are NOT the same thing. Domain invariants protect business consistency. Input validation protects against bad input.

## Domain Methods

```csharp
// Domain method pattern
public void UpdatePrice(Money newPrice)
{
    // invariant check
    if (newPrice.Amount <= 0) throw new InvalidCarPrice();

    // state change
    Price = newPrice;
    UpdatedAt = DateTime.UtcNow;

    // side effect within aggregate boundary
    _priceHistory.Add(new CarPriceHistory(Id, newPrice, UpdatedAt.Value));
}
```

## Entity Lifecycle (mutability rules)
- Properties: `private set` — changed only through domain methods
- Collections: private backing field with public `IReadOnlyCollection` or `List` exposed read-only
- State transitions through explicit methods (e.g. `Delete()`, `UpdatePrice()`, `Activate()`)
- No direct property assignment from outside the entity

## Domain Exceptions
- Every domain rule violation should have its own custom exception
- Custom exceptions inherit from `DomainException` (base class)
- Prefer specific exceptions: `InvalidCarPrice`, `MaxPhotosExceeded`, `CarAlreadyDeleted`
- Do NOT use generic `DomainException` directly — always create a specific subclass
- Exception names should describe the violated rule — do NOT include the word "Exception" in the name

```csharp
public class InvalidCarPrice : DomainException
{
    public InvalidCarPrice() : base("Price must be greater than zero.") { }
}
```

## Domain Events (PLANNED — not yet implemented)
- Aggregates can raise Domain Events for side effects outside their boundary
- Side effects that cross aggregate boundaries MUST use Domain Events
- Events are raised within domain methods, dispatched after persistence
- Keep events simple — they carry data, not behavior

```csharp
// Raising an event in aggregate
public void UpdatePrice(Money newPrice)
{
    Price = newPrice;
    UpdatedAt = DateTime.UtcNow;
    _priceHistory.Add(new CarPriceHistory(Id, newPrice, UpdatedAt.Value));

    AddDomainEvent(new CarPriceChangedEvent(Id, newPrice));
}
```

## Soft Delete
- Use soft delete ONLY when required by business or audit requirements
- Not a universal rule — evaluate per aggregate
- When used: `IsDeleted` flag + `Delete()` method + EF `HasQueryFilter`
- When not needed: physical delete is acceptable

## Repository Pattern
- Only repositories load and save aggregates
- Repository interface defined in Application layer
- Repository implementation in Infrastructure layer
- No direct `DbContext` usage in Application layer
- One repository per Aggregate Root
- Repositories work with aggregates, not individual entities

## CQRS (Command / Query)

### Commands (write)
- Implement `ICommand<T>`
- Handler orchestrates: fetch aggregate → call domain method → save
- Handler MUST NOT contain business logic
- Can return Id of created resource or a result DTO — pragmatic approach

### Queries (read)
- Implement `IQuery<T>`
- Handler fetches data and maps to DTO
- MUST NOT modify state
- Returns DTOs, never domain entities
- Use `AsNoTracking()` for performance

### Acceptable exceptions
- Login query returning a token is acceptable
- Returning a DTO after create/update is acceptable (pragmatic CQRS)

## What NOT to do (negative rules for AI)
- Do NOT put business logic in handlers — delegate to domain methods
- Do NOT use `DbContext` directly in Application layer
- Do NOT reference Infrastructure from Domain
- Do NOT use generic `DomainException` — create specific exceptions
- Do NOT make aggregates too large — keep consistency boundaries small
- Do NOT reference other aggregates by object — use Id only
- Do NOT add EF attributes or annotations in Domain entities
- Do NOT skip invariant validation in constructors or domain methods
- Do NOT create public setters on domain entities
