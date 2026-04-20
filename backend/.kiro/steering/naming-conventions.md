---
inclusion: always
---

# Project Naming Conventions

## Commands
- Pattern: `Create{EntityName}Command`, `Update{EntityName}Command`, `Delete{EntityName}Command`
- Examples: `CreateCarCommand`, `UpdateUserCommand`

## Queries
- Pattern: `Get{EntityName}Query`, `Get{EntityNamePlural}Query`
- Examples: `GetCarQuery`, `GetCarsQuery`

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
- Domain: inherits from `DomainException` — specific name per rule: `InvalidPriceException`
- Infrastructure: inherits from `InfrastructureException`

## DTOs
- Pattern: `{EntityName}Response`, `{EntityName}ListResponse`
- Request records: `Create{EntityName}Request`, `Update{EntityName}Request`
