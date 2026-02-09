namespace CarMarketplace.API.Middleware;

public static class MiddlewareExtensions
{
    public static void UseGlobalExceptionHandlingMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<GlobalExceptionMiddleware>();
}