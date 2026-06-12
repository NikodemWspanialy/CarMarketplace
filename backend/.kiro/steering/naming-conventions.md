---
inclusion: always
---

# Project Naming Conventions

## Commands
- Pattern: `Create{EntityName}Request`, `Update{EntityName}Request`, `Delete{EntityName}Request`
- The command record doubles as the API body DTO — no separate body DTOs needed
- Controller injects route params via `body with { ParamName = value }`
- Examples: `CreateCarRequest`, `UpdateUserRequest`, `AddCarPhotoRequest`

## Queries
- Pattern: `Get{EntityName}Request`, `Get{EntityNamePlural}Request`
- Examples: `GetCarRequest`, `GetCarsRequest`

## Handlers
- Pattern: `{Operation}{EntityName}Handler`
- Examples: `CreateCarHandler`, `GetCarsHandler`

## Repositories
- Interface: `I{EntityName}Repository` → Implementation: `{EntityName}Repository`
- Examples: `ICarRepository` → `CarRepository`

## EF Configurations
- Pattern: `{EntityName}Configuration`
- Examples: `CarConfiguration`, `UserConfiguration`

## Exceptions
- Do NOT include the word "Exception" in exception class names
- Domain: inherits from `DomainException` — specific name per rule: `InvalidCarPrice`, `CarAlreadyDeleted`
- Infrastructure: inherits from `InfrastructureException`

## DTOs
- `{EntityName}Response` — lightweight DTO for list items (minimal fields)
- `{EntityName}DetailsResponse` — full DTO for single entity (all fields)
- `ListResponse<T>` — generic paged collection wrapper (Items, TotalCount, PageNumber, PageSize) in `Application/Common/DTOs/`

## Factories
- Interface + implementation in a single file: `I{EntityName}Factory` → `{EntityName}Factory`
- `Create` method accepts the command request directly (e.g., `Create(CreateCarRequest request)`)
- Value object factories: `IMoneyFactory` → `MoneyFactory`
- Located in `Application/{EntityNamePlural}/Factories/`

## Searchers
- Pattern: `I{EntityName}Searcher` → `{EntityName}Searcher`
- Located in `Application/{EntityNamePlural}/Searchers/`

## Guards
- Pattern: `I{EntityName}SellerGuard` → `{EntityName}SellerGuard`
- Located in `Application/{EntityNamePlural}/Helpers/`

## Shared Services
- Pattern: `I{EntityName}Service` → `{EntityName}Service`
- One service per aggregate for common operations shared between user and admin handlers
- Located in `Application/{EntityNamePlural}/Helpers/`

## Policy Constants
- Pattern: `{Concern}Policy` — internal static class with `internal const string` members
- Located in `API/Common/`
- Examples: `AuthPolicy.AdminOnly`, `RateLimitPolicy.Auth`, `CorsPolicy.AllowFrontend`
- No magic strings in controllers or Program.cs — always reference policy constants

## Shared Validator Extensions
- Pattern: `{FieldName}ValidatorExtensions` with extension method `Valid{FieldName}<T>()`
- Located in `Application/Common/Validators/`
- Example: `PasswordValidatorExtensions.ValidPassword()`, `PagingValidatorExtensions.ValidPaging()`

## Domain Validators (Application layer)
- Pattern: `I{Operation}{EntityName}Validator` → `{Operation}{EntityName}Validator`
- Async business rule checks that require DB access (e.g., uniqueness, limits) — called from handlers
- Throw domain exceptions on violation
- NOT FluentValidation — these are separate interfaces for cross-aggregate or DB-dependent rules
- Located in `Application/{EntityNamePlural}/Validators/`
