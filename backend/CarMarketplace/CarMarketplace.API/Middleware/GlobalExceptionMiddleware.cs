using System.Net;
using System.Text.Json;
using CarMarketplace.Domain.Exceptions;
using FluentValidation;

namespace CarMarketplace.API.Middleware;

public class GlobalExceptionMiddleware(
    //ILogger logger,
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
            // logger.LogError(ex, ex.Message); // TODO add new middleware with logging for every call
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(
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

    private static ErrorResponse HandleValidationException(ValidationException exception)
    {
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray());

        return new(exception.Message, (int)HttpStatusCode.BadRequest, errors);
    }

    private static ErrorResponse HandleDomainException(DomainException exception) =>
        new(exception.Message, (int)HttpStatusCode.BadRequest);

    private static ErrorResponse HandleUnauthorizedException(UnauthorizedAccessException exception) =>
        new(exception.Message, (int)HttpStatusCode.Unauthorized);

    private static ErrorResponse HandleUnknownException(Exception exception) =>
        new(exception.Message, (int)HttpStatusCode.InternalServerError);
}