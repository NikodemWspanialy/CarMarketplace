using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.UpdatePhotosOrder;

public record UpdatePhotosOrderRequest(Guid CarId, List<PhotoOrderItem> Photos) : ICommand<Unit>;

public record PhotoOrderItem(Guid Id, int NewOrder);
