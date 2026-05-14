using CarMarketplace.Application.Cars.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Cars.Commands.AddCarPhotos;

public record AddCarPhotosRequest(Guid CarId, List<AddCarPhotosItem> Photos) : ICommand<IReadOnlyList<CarPhotoResponse>>;

public abstract record AddCarPhotosItem(string Url, int Order, bool IsPrimary);
