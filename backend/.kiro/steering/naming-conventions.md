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
- Domain: inherits from `DomainException` — specific name per rule: `InvalidPriceException`
- Infrastructure: inherits from `InfrastructureException`

## DTOs
- Pattern: `{EntityName}Response`, `{EntityName}ListResponse`
