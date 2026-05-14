using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Commands.AddCarPhoto;

public record AddCarPhotoRequest(Guid CarId, string Url, int Order, bool IsPrimary) : ICommand<CarPhotoResponse>;
