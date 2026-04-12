---
inclusion: always
---

# CarMarketplace Project Architecture

## Architectural Style
- Clean Architecture + DDD + CQRS (MediatR)
- Modular Monolith — single deployment, logical modules

## Layers (from inside out)

### Domain (no external dependencies)
- Entities for example: `Car` (Aggregate Root), `CarPhoto`, `CarPriceHistory`, `User`
- Aggregate Roots implement `IAggregateRoot` interface
- Value Objects mapped via `OwnsOne` in EF Core (for example: `Money`)
- Enums for example: `FuelType`, `UserRole`
- Domain exceptions inherit from `DomainException`
- Soft delete via `IsDeleted` flag
- File structure: `Domain/{EntityNamePlural}/{EntityName}.cs`
  - Example: `Domain/Cars/Car.cs`, `Domain/Users/User.cs`

### Application
- Commands and Queries (CQRS) via MediatR
- `ICommand<T>` → write operations, `IQuery<T>` → read operations
- Validation: FluentValidation
- Exceptions inherit from `DomainException`
- Pipeline Behaviors: `LoggerBehavior`, `UnitOfWorkBehavior`
- File structure: `Application/{EntityNamePlural}/`
  - `Commands/` — write operations (create, update, delete)
  - `Queries/` — read operations
  - `DTOs/` — data transfer objects
  - `Exceptions/` — application-level exceptions
  - `Helpers/` — utility interfaces and classes
  - `Validators/` — FluentValidation validators
  - `Factories/` — entity factories
  - `Repositories/` — repository interfaces
  - Not all subdirectories are required — create only what's needed

### Infrastructure
- EF Core + PostgreSQL
- Fluent API configurations in `Persistence/Configurations/`
- `OwnsOne` for Value Objects
- A configuration class must be defined for every entity that has a DbSet in DbContext
- Repositories implement interfaces from Application
- Infrastructure exceptions inherit from `InfrastructureException`
- JWT (BeaverJwtProvider), BCrypt (BCryptPasswordHasher)
- UnitOfWork as pipeline behavior

### API
- ASP.NET Core Web API
- Controllers in `Controllers/` folder
- `GlobalExceptionMiddleware` catches `DomainException` and `InfrastructureException`
- Swagger with JWT Bearer auth

## Naming Conventions

### Commands
- Pattern: `Create{EntityName}Command`, `Update{EntityName}Command`, `Delete{EntityName}Command`
- Examples:
  - `CreateCarCommand`
  - `UpdateUserCommand`

### Queries
- Pattern: `Get{EntityName}Query`, `Get{EntityNamePlural}Query`
- Examples:
  - `GetCarQuery`
  - `GetCarsQuery`

### Handlers
- Pattern: `{Operation}{EntityName}Handler`
- Examples:
  - `CreateCarHandler`
  - `GetCarsHandler`

### Repositories
- Interface: `I{EntityName}Repository` → Implementation: `{EntityName}Repository`
- Examples:
  - `ICarRepository` → `CarRepository`

### EF Configurations
- Pattern: `{EntityName}Configuration`
- Examples:
  - `CarConfiguration`
  - `UserConfiguration`

### Exceptions
- Domain: inherits from `DomainException`
- Infrastructure: inherits from `InfrastructureException`
