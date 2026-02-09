using System.Net;
using System.Text.Json;
using CarMarketplace.Domain.Exceptions;

namespace CarMarketplace.API.Middleware;

public class GlobalExceptionMiddleware(
    ILogger logger,
    RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message); // TODO add new middleware with logging for every call
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = exception switch
        {
            DomainException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        var response = new ErrorResponse(exception.Message, (int)statusCode, null);

        var json = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(json);
    }
}