using MediatR;

namespace CarMarketplace.Application.Common.Abstractions;

public interface ICommand<out T> : IRequest<T>;