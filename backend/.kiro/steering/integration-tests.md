---
inclusion: always
---

# Integration Tests

## Project
- `CarMarketplace.IntegrationTests` — references API and Infrastructure
- Namespace: `CarMarketplace.IntegrationTests`

## Stack
- xUnit 2.9 — test framework
- Microsoft.AspNetCore.Mvc.Testing — `WebApplicationFactory<Program>` for in-process API hosting
- Testcontainers.PostgreSql — real PostgreSQL container per test class
- Respawn — fast database reset between tests
- FluentAssertions — readable assertions
- Bogus — test data generation

## Architecture
- Tests send real HTTP requests through the full pipeline (controller → MediatR → handler → EF → PostgreSQL)
- No in-memory database fakes — always real PostgreSQL via Docker
- External services (e.g. `IEmailSender`) are mocked in the factory

## File Structure
```
IntegrationTests/
├── Common/
│   ├── CarMarketplaceApiFactory.cs   — custom WebApplicationFactory + Testcontainers
│   ├── IntegrationTestBase.cs        — abstract base class with Respawn + auth helpers
│   └── JwtTokenGenerator.cs          — generates valid JWT tokens for tests
└── {Feature}/
    └── {Feature}Tests.cs             — test classes grouped by feature
```

## Key Classes

### CarMarketplaceApiFactory
- Inherits `WebApplicationFactory<Program>`, implements `IAsyncLifetime`
- Starts PostgreSQL container, applies migrations on `InitializeAsync`
- Swaps `DbContextOptions<CarMarketplaceDbContext>` to point at container
- Shared per test class via `IClassFixture<CarMarketplaceApiFactory>`

### IntegrationTestBase
- Abstract base class for all integration tests
- Implements `IClassFixture<CarMarketplaceApiFactory>` + `IAsyncLifetime`
- Resets database via Respawn before each test
- Provides `HttpClient`, `CreateDbContext()`, `Authenticate()`, `AuthenticateAsAdmin()`

### JwtTokenGenerator
- Static helper generating tokens matching `appsettings.json` JWT config
- Used by `Authenticate()` / `AuthenticateAsAdmin()` in base class

## Conventions
- One test class per feature/endpoint group
- Test method naming: `{Action}_With{Condition}_Returns{Expected}` or `Should_{Behavior}`
- Always use Arrange/Act/Assert pattern
- Use `Client` (HttpClient) for HTTP requests — never call handlers directly
- Use `CreateDbContext()` only for assertions (verifying DB state), not for setup via EF
- Setup data through API calls or direct DB inserts when API path is too complex
- Primary constructor for injecting `CarMarketplaceApiFactory`

## Running Tests
- Requires Docker running locally
- `dotnet test CarMarketplace.IntegrationTests`
