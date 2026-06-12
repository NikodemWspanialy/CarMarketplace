using MediatR;
using Microsoft.Extensions.Logging;

namespace CarMarketplace.Application.Common.Behaviors;

public class LoggerBehavior<TRequest, TResponse>(
    ILogger<LoggerBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling {RequestName}", requestName);
        
        var response = await next(token);

        logger.LogInformation("Handled {RequestName}", requestName);
        
        return response;
    }
}