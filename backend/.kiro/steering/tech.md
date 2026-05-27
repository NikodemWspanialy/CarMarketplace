---
inclusion: always
---

# Technology Stack

## Runtime & Language
- .NET 10 / C# 13
- `<Nullable>enable</Nullable>` — explicit nullability everywhere
- `<ImplicitUsings>enable</ImplicitUsings>`
- Target OS: Linux (Docker)

## Framework
- ASP.NET Core Web API
- MediatR 14.0.0 — CQRS command/query dispatching and pipeline behaviors
- FluentValidation 12.1.1 — input validation in Application layer

## Database & ORM
- PostgreSQL
- Entity Framework Core 10.0.2 (Npgsql provider 10.0.0)
- Fluent API configuration only — no data annotations in Domain
- EF Core Design + Tools for migrations

## Authentication & Security
- JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer 10.0.2)
- BCrypt.Net-Next 4.0.3 — password hashing

## API Documentation
- Swashbuckle.AspNetCore 10.1.2 — Swagger UI with JWT Bearer security definition
- Microsoft.AspNetCore.OpenApi 10.0.2

## Infrastructure
- Docker Compose for local development
- PostgreSQL container (port 5432)

## Testing
- xUnit 2.9.3 — test framework
- Microsoft.AspNetCore.Mvc.Testing — `WebApplicationFactory` for in-process API hosting
- Testcontainers.PostgreSql — real PostgreSQL in Docker per test run
- Respawn — fast database reset between tests (no drop/recreate)
- FluentAssertions — readable assertions
- Bogus — test data generation

## Project References
- Domain — pure C#, zero NuGet dependencies
- Application → Domain
- Infrastructure → Application, Domain
- API → Application, Infrastructure
- API.tests → API, Infrastructure
- IntegrationTests → API, Infrastructure, Tests.Shared
- Tests.Shared → Application
