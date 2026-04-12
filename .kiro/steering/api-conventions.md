---
inclusion: fileMatch
fileMatchPattern: "**/Controllers/**,**/API/**"
---

# API Conventions in CarMarketplace

## Controllers
- Inherit from `ControllerBase` with `[ApiController]` attribute
- Route: `[Route("api/resource")]`
- DI via primary constructor: `public class XController(IMediator mediator)`
- Controller contains NO logic — only `mediator.Send(request)`

## Response Patterns
- Always leave an empty line before `return` statements for readability

```csharp
// POST — create
[HttpPost("create")]
public async Task<IActionResult> Create([FromBody] CreateXRequest command)
{
    var id = await mediator.Send(command);

    return CreatedAtAction(nameof(GetById), new { id }, null);
}

// PUT — update (returns updated resource)
[HttpPut("update/{id:guid}")]
public async Task<IActionResult> Update(Guid id, [FromBody] UpdateXRequest command)
{
    if (id != command.Id) return BadRequest("Id mismatch");
    var result = await mediator.Send(command);

    return Ok(result);
}

// DELETE — soft delete
[HttpDelete("delete/{id:guid}")]
public async Task<IActionResult> Delete(Guid id)
{
    await mediator.Send(new DeleteXRequest(id));

    return NoContent();
}

// GET — single
[HttpGet("get-details/{id:guid}")]
public async Task<IActionResult> GetById(Guid id)
{
    var result = await mediator.Send(new GetXRequest(id));

    return Ok(result);
}

// GET — paged list
[HttpGet("get-details-list")]
public async Task<IActionResult> GetPaged([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var result = await mediator.Send(new GetXsRequest(pageNumber, pageSize));

    return Ok(result);
}
```

## Authentication
- JWT Bearer token in `Authorization: Bearer <token>` header
- `[Authorize]` on controllers/actions requiring auth
- Swagger configured with JWT security definition

## Error Handling
- `GlobalExceptionMiddleware` catches all exceptions
- `DomainException` → 400 Bad Request
- `UnauthorizedAccessException` → 401 Unauthorized
- Other → 500 Internal Server Error
- Response format: `ErrorResponse(message, statusCode, details)`

## Endpoints
- Auth: `POST /api/auth/register`, `POST /api/auth/login`
- Cars: `POST /api/car/create`, `PUT /api/car/update-details/{id}`, `DELETE /api/car/delete/{id}`
- Cars query: `GET /api/car/get-details/{id}`, `GET /api/car/get-details-list`
