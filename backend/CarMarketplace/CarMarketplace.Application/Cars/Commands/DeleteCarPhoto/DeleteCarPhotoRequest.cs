using CarMarketplace.Application.Common.Abstractions;
using MediatR;

namespace CarMarketplace.Application.Cars.Commands.DeleteCarPhoto;

public record DeleteCarPhotoRequest(Guid CarId, Guid PhotoId) : ICommand<Unit>;
