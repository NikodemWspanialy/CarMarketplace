---
inclusion: always
---

# Coding Standards

## Language & Runtime
- C# 13 / .NET 10
- Use latest language features (primary constructors, collection expressions, etc.)
- PostgreSQL with EF Core
- Always use `async/await` — never `.Result()` or `.Wait()`
- Dependency Injection everywhere — no manual instantiation of services
- Layer separation: API / Application / Domain / Infrastructure

## Nullable
- `<Nullable>enable</Nullable>` is on
- Always distinguish `string` vs `string?` — be explicit about nullability

## Naming Conventions

| Element | Style | Example |
|---|---|---|
| Classes, interfaces, structs, enums | PascalCase | `CustomerOrder` |
| Properties | PascalCase | `FirstName` |
| Methods | PascalCase | `CalculateTotal` |
| Private fields | _camelCase | `_orderCount` |
| Local variables | camelCase | `totalPrice` |
| Method parameters | camelCase | `userId` |
| Interfaces | I + PascalCase | `IRepository` |
| Constants (`const`) | PascalCase | `MaxItems` |
| Static readonly fields | PascalCase | `DefaultTimeout` |
| Namespaces | PascalCase, match project structure | `Company.Product.Module` |
| Files | Match class name (PascalCase) | `CustomerOrder.cs` |
| Async methods | Suffix with `Async` | `GetDataAsync` |
| Events | PascalCase with verb | `Completed` |
| Delegates | PascalCase | `ProcessCompletedHandler` |
| Generic types | T + name | `TItem` |
| Booleans | Prefix with Is, Has, Can | `IsActive` |

- Avoid abbreviations — use full, readable names (`customerAddress`, not `custAddr`)
- Read-only properties — prefer `get;` over public fields

## CancellationToken
- Always pass `CancellationToken` through the call chain
- Parameter name must be `token` (not `cancellationToken`)

## `var` Usage
- Use `var` everywhere when the type is obvious from the right side

## Braces
- `{}` are not required for single-line blocks (e.g. `if`, `foreach`)
- For single-line methods use expression body (`=>`) syntax

## Using Statements
- All usings at the top of the file
- `System.*` namespaces first, then the rest alphabetically

## Access Modifiers
- Restrict access as much as possible
- `internal` for implementation details (EF configurations, etc.)
- `public` only where necessary
- `sealed` on handlers is not required

## Constructors
- Use primary constructors where possible

## Class Member Order
1. Private fields
2. Properties
3. Constructors
4. Public methods
5. Private methods

## Comments
- Short, concise comments in English
- Don't over-explain — comment the "why", not the "what"

## LINQ
- Always use method syntax (`Where().Select()`) — no query syntax

## Clean Code Principles
- Methods should be short and do one thing
- Classes should have a single responsibility
- Names must be readable and unambiguous (no abbreviations)
- Avoid duplication (DRY)
- Avoid God Objects — keep classes focused
- Use guard clauses (early returns) instead of deep nesting
- Keep business logic out of the UI/API layer
- Maintain consistent project structure

## SOLID
- Single Responsibility — one reason to change per class
- Open/Closed — extend behavior without modifying existing code
- Liskov Substitution — subtypes must be substitutable
- Interface Segregation — small, focused interfaces
- Dependency Inversion — depend on abstractions, not concretions
