namespace CarMarketplace.Application.Cars.DTOs;

public record CarListResponse(IReadOnlyList<CarResponse> Items, int TotalCount);
