using MediatR;

namespace CarMarketplace.Application.Common.Behaviors;

public class LoggerBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
    {
        var requestLog = request.ToString();
        Console.WriteLine("start: " + requestLog);
        
        var response = await next(token);

        Console.WriteLine("end: " + requestLog);
        
        return response;
    }
}