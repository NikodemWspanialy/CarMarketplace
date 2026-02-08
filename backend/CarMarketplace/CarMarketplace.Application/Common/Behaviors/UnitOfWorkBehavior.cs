using CarMarketplace.Application.Common.Abstractions;
using CarMarketplace.Application.Common.Interfaces;
using MediatR;

namespace CarMarketplace.Application.Common.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
    {
        var response = await next(token);

        if (request is ICommand)
        {
            await unitOfWork.SaveChangesAsync(token);
        }
        
        return response;
    }
}