using MediatR;

namespace CarMarketplace.Application.Common.Abstractions;

public interface IQuery;

public interface IQuery<out T> : IRequest<T>, IQuery;