---
inclusion: fileMatch
fileMatchPattern: "**/Controllers/**"
---

# API Conventions in CarMarketplace

## Controllers
- Inherit from `ControllerBase` with `[ApiController]` attribute
- Route: `[Route("api/resource")]`
- DI via primary constructor: `public class XController(IMediator mediator)`
- Controller contains NO logic — only `mediator.Send(request)`
- Debug/internal controllers or endpoints: hide from Swagger with `[ApiExplorerSettings(IgnoreApi = true)]`

## CancellationToken
- Every controller action MUST accept `CancellationToken token` as the last parameter
- Pass `token` to `mediator.Send()` — it propagates through the entire pipeline (handlers, validators, repositories, EF Core queries)

## Response Patterns
- Endpoints NEVER return Domain entities — always use `{EntityName}Response` DTOs defined in Application layer
- Always leave an empty line before `return` statements for readability

```csharp
// POST — create
[HttpPost("create")]
public async Task<IActionResult> Create([FromBody] CreateXRequest command, CancellationToken token)
{
    var id = await mediator.Send(command, token);

    return CreatedAtAction(nameof(GetById), new { id }, null);
}

// POST — create (child entity without own GET endpoint)
[HttpPost("{parentId:guid}/children")]
public async Task<IActionResult> AddChild(Guid parentId, [FromBody] AddChildRequest body, CancellationToken token)
{
    var result = await mediator.Send(body with { ParentId = parentId }, token);

    return StatusCode(StatusCodes.Status201Created, result);
}

// PUT — update (returns updated resource)
[HttpPut("update/{id:guid}")]
public async Task<IActionResult> Update(Guid id, [FromBody] UpdateXRequest command, CancellationToken token)
{
    if (id != command.Id) return BadRequest("Id mismatch");
    var result = await mediator.Send(command, token);

    return Ok(result);
}

// DELETE — soft delete
[HttpDelete("delete/{id:guid}")]
public async Task<IActionResult> Delete(Guid id, CancellationToken token)
{
    await mediator.Send(new DeleteXRequest(id), token);

    return NoContent();
}

// PATCH — status transition (no body, action in route)
[HttpPatch("{id:guid}/action-name")]
public async Task<IActionResult> ActionName(Guid id, CancellationToken token)
{
    await mediator.Send(new ActionNameRequest(id), token);

    return NoContent();
}

// GET — single
[HttpGet("get-details/{id:guid}")]
public async Task<IActionResult> GetById(Guid id, CancellationToken token)
{
    var result = await mediator.Send(new GetXRequest(id), token);

    return Ok(result);
}

// GET — paged list
[HttpGet("get-details-list")]
public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken token = default)
{
    var result = await mediator.Send(new GetXsRequest(pageNumber, pageSize), token);

    return Ok(result);
}
```

## Authentication
- JWT Bearer token in `Authorization: Bearer <token>` header
- Controllers where all endpoints require auth: `[Authorize]` at class level (e.g., UserController, AdminController)
- Mixed controllers (public + auth): `[Authorize]` per endpoint (e.g., CarController)
- Swagger configured with JWT security definition

## Authorization Policies
- Policy-based authorization for role-restricted controllers
- `AdminOnly` policy — requires `Admin` role, applied at controller level with `[Authorize(Policy = "AdminOnly")]`

## Rate Limiting
- Built-in `AddRateLimiter` with named policies, partitioned per IP
- Policy `"auth"` — fixed window, 5 requests/min per IP, applied to sensitive auth endpoints (login, forgot-password, reset-password)
- Apply via `[EnableRateLimiting("policyName")]` on individual actions
- `app.UseRateLimiter()` placed before `UseAuthentication` in pipeline
- Rejection returns 429 Too Many Requests

## CORS
- Named policy "AllowFrontend" — allowed origins from `appsettings.json` → `Cors:AllowedOrigins`
- Any headers, any methods allowed
- `UseCors` placed before `UseRateLimiter` in pipeline

## Error Handling
- `GlobalExceptionMiddleware` catches all exceptions
- `FluentValidation.ValidationException` → 400 Bad Request with field-level errors
- `DomainException` → 400 Bad Request
- `UnauthorizedAccessException` → 401 Unauthorized
- `InfrastructureException` → 500 Internal Server Error with generic message — never expose infrastructure details to client
- Other → 500 Internal Server Error with generic message — never expose exception details to client
- All exceptions logged via `ILogger` — `LogWarning` for expected (validation, domain, unauthorized), `LogError` for unknown (500)
- Response format: `ErrorResponse(message, statusCode, errors?)`
- Validation error response includes `errors` dictionary: `{ "fieldName": ["message1", "message2"] }`
- JSON serialized with `camelCase` property naming

## Endpoints
- Auth: `POST /api/auth/register`, `POST /api/auth/login`, `POST /api/auth/forgot-password`, `POST /api/auth/reset-password`, `POST /api/auth/refresh-token`, `POST /api/auth/logout`
- Cars: `POST /api/car/create`, `PUT /api/car/update-details/{id}`, `PUT /api/car/update-price/{id}`, `DELETE /api/car/delete/{id}`
- Cars query: `GET /api/car/get-details/{id}`, `GET /api/car/get-details-list`
- Car photos: `POST /api/car/{carId}/photos`, `POST /api/car/{carId}/photos/batch`, `DELETE /api/car/{carId}/photos/{photoId}`, `PUT /api/car/{carId}/photos/{photoId}/set-primary`, `PUT /api/car/{carId}/photos/update-order`
- Users: `GET /api/user/profile`, `PUT /api/user/update-profile`, `PUT /api/user/change-password`, `PUT /api/user/change-email`, `DELETE /api/user/delete-account`
- Admin: `GET /api/admin/user/{id}`, `GET /api/admin/users`, `GET /api/admin/user/{id}/ban-history`, `PUT /api/admin/upgrade-to-admin/{id}`, `PUT /api/admin/downgrade-to-user/{id}`, `PUT /api/admin/update-user-profile/{id}`, `PUT /api/admin/change-user-password/{id}`, `DELETE /api/admin/delete-user/{id}`, `PUT /api/admin/ban-user/{id}`, `PUT /api/admin/unban-user/{id}`, `PATCH /api/admin/listings/{id}/feature`, `PATCH /api/admin/listings/{id}/remove-feature`
- Contacts: `POST /api/user/contacts`, `GET /api/user/contacts`, `PUT /api/user/contacts/{id}`, `DELETE /api/user/contacts/{id}`
- Listings: `POST /api/listing/create`, `GET /api/listing/get-details/{id}`, `GET /api/listing/get-details-list`, `PUT /api/listing/update-title/{id}`, `DELETE /api/listing/delete/{id}`, `PATCH /api/listing/{id}/mark-as-sold`, `PATCH /api/listing/{id}/archive`, `PATCH /api/listing/{id}/deactivate`, `PATCH /api/listing/{id}/reactivate`, `POST /api/listing/{id}/contacts/{contactId}`, `DELETE /api/listing/{id}/contacts/{contactId}`, `POST /api/listing/{id}/contacts/reveal`, `GET /api/listing/{id}/stats`
