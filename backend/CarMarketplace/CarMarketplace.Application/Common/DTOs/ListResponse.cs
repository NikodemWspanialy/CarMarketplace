namespace CarMarketplace.Application.Common.DTOs;

public record ListResponse<T>(
    IReadOnlyCollection<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize);
