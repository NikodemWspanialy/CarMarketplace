using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Commands.SetPrimaryCarPhoto;

public record SetPrimaryCarPhotoRequest(Guid CarId, Guid PhotoId) : ICommand<CarPhotoResponse>;
