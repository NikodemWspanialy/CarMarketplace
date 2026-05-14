---
inclusion: always
---

# Project Naming Conventions

## Commands
- Pattern: `Create{EntityName}Request`, `Update{EntityName}Request`, `Delete{EntityName}Request`
- Examples: `CreateCarRequest`, `UpdateUserRequest`

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
- `{EntityName}ListResponse` — collection wrapper with pagination metadata

## Factories
- Interface + implementation: `I{EntityName}Factory` → `{EntityName}Factory`
- Value object factories: `IMoneyFactory` → `MoneyFactory`
- Located in `Application/{EntityNamePlural}/Factories/`

## Searchers
- Pattern: `I{EntityName}Searcher` → `{EntityName}Searcher`
- Located in `Application/{EntityNamePlural}/Searchers/`

## Guards
- Pattern: `I{EntityName}SellerGuard` → `{EntityName}SellerGuard`
- Located in `Application/{EntityNamePlural}/Helpers/`
