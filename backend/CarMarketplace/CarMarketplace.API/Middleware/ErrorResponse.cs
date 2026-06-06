namespace CarMarketplace.API.Middleware;

public record ErrorResponse(
    string Message,
    int StatusCode,
    Dictionary<string, string[]>? Errors = null);