using System.Net;
using System.Text.Json;
using CarMarketplace.Domain.Exceptions;
using FluentValidation;

namespace CarMarketplace.API.Middleware;

public class GlobalExceptionMiddleware(
    ILogger<GlobalExceptionMiddleware> logger,
    RequestDelegate next)
{
    private static readonly JsonSerializerOptions serializationOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var response = exception switch
        {
            ValidationException ex => HandleValidationException(ex),
            DomainException ex => HandleDomainException(ex),
            UnauthorizedAccessException ex => HandleUnauthorizedException(ex),
            _ => HandleUnknownException(exception)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.StatusCode;

        var json = JsonSerializer.Serialize(response, serializationOptions);

        return context.Response.WriteAsync(json);
    }

    private ErrorResponse HandleValidationException(ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        logger.LogWarning(exception, "Validation failed: {@Errors}", errors);

        return new(exception.Message, (int)HttpStatusCode.BadRequest, errors);
    }

    private ErrorResponse HandleDomainException(DomainException exception)
    {
        logger.LogWarning(exception, "Domain exception: {Message}", exception.Message);

        return new(exception.Message, (int)HttpStatusCode.BadRequest);
    }

    private ErrorResponse HandleUnauthorizedException(UnauthorizedAccessException exception)
    {
        logger.LogWarning(exception, "Unauthorized access: {Message}", exception.Message);

        return new(exception.Message, (int)HttpStatusCode.Unauthorized);
    }

    private ErrorResponse HandleUnknownException(Exception exception)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        return new(exception.Message, (int)HttpStatusCode.InternalServerError);
    }
}