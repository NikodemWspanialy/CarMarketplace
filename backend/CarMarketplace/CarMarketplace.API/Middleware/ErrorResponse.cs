namespace CarMarketplace.API.Middleware;

public record ErrorResponse(string Message, int StatusCode, string? Details);