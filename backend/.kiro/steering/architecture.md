---
inclusion: always
---

# CarMarketplace Project Architecture

## Architectural Style
- Clean Architecture + DDD + CQRS
- Modular Monolith — single deployment, logical modules

## Layers (from inside out)

### Domain (no external dependencies)
- Entities for example: `Car` (Aggregate Root), `CarPhoto`, `CarPriceHistory`, `User`
- Aggregate Roots implement `IAggregateRoot` interface
- Each aggregate has a steering file in `.kiro/steering/aggregates/{name}.md` documenting actions, business rules, and authorization
- Value Objects (for example: `Money`)
- Enums for example: `FuelType`, `UserRole`
- Domain exceptions inherit from `DomainException`
- Soft delete via `IsDeleted` flag
- File structure: `Domain/{EntityNamePlural}/{EntityName}.cs`
  - Example: `Domain/Cars/Car.cs`, `Domain/Users/User.cs`

### Application
- NEVER return Domain entities from handlers — always map to a `{EntityName}Response` DTO defined in Application layer
- Commands and Queries (CQRS) — `ICommand<T>` → write operations, `IQuery<T>` → read operations
- Exceptions inherit from `DomainException`
- Pipeline Behaviors: `LoggerBehavior`, `UnitOfWorkBehavior`
- File structure: `Application/{EntityNamePlural}/`
  - `Commands/` — write operations (create, update, delete)
  - `Queries/` — read operations
  - `DTOs/` — data transfer objects
  - `Exceptions/` — application-level exceptions
  - `Helpers/` — seller guards, utility interfaces
  - `Searchers/` — entity searchers (find by id or throw)
  - `Validators/` — FluentValidation validators
  - `Factories/` — entity and value object factories
  - `Repositories/` — repository interfaces
  - Not all subdirectories are required — create only what's needed
- Shared code in `Application/Common/`:
  - `Abstractions/` — `ICommand`, `IQuery` interfaces
  - `Behaviors/` — `LoggerBehavior`, `UnitOfWorkBehavior`
  - `Interfaces/` — `IUnitOfWork`, `ICurrentUserProvider`
- Admin operations in `Application/Admin/` — commands restricted to admin role

### Infrastructure
- Fluent API configurations in `Persistence/Configurations/`
- `OwnsOne` for Value Objects
- Only Aggregate Roots get a `DbSet` in DbContext — child entities are accessed through the root's navigation properties
- Standalone entities (not aggregates, not children) may also get a `DbSet` when they need to be queried independently (e.g., `PasswordResetToken`)
- A configuration class must be defined for every entity persisted to its own table (regardless of DbSet)
- Child entity relationships defined via `HasMany` with strongly-typed navigation in the Aggregate Root's configuration
- Repositories implement interfaces from Application
- Infrastructure exceptions inherit from `InfrastructureException`
- UnitOfWork as pipeline behavior
- Security: `CurrentUserProvider`, `UserRoleMapper`, password hasher, JWT provider in `Security/`

### API
- Controllers in `Controllers/` folder
- `GlobalExceptionMiddleware` catches `DomainException` and `InfrastructureException`

### Integration Tests (`CarMarketplace.IntegrationTests`)
- Real PostgreSQL via Testcontainers — no in-memory fakes
- `WebApplicationFactory<Program>` hosts full API pipeline in-process
- Respawn resets DB between tests (DELETE, not DROP — fast)
- Base classes in `Common/IntegrationTestBases/`:
  - `IntegrationTestBase` — Respawn, `Client`, `TestData` (DbContext), `SendAsync<T>()`, `Authenticate()`
  - `IntegrationTestBaseWithUserLogin` — registers + authenticates as User
  - `IntegrationTestBaseWithAdminLogin` — registers + promotes + authenticates as Admin
- `JwtTokenGenerator` — standalone token generation for tests (independent of `IJwtProvider`)
- Test files grouped by feature: `{Feature}/{Feature}Tests.cs`
- Conventions: Arrange/Act/Assert, primary constructors, `Client` for HTTP, `SendAsync` for MediatR dispatch, `TestData` for DB assertions
- Requires Docker running locally

See `naming-conventions.md` for all naming patterns (commands, queries, handlers, repositories, etc.).
