using CarMarketplace.Application.Admin.DTOs;
using CarMarketplace.Application.Common.Abstractions;

namespace CarMarketplace.Application.Admin.Queries.GetBanHistory;

public record GetBanHistoryRequest(Guid UserId) : IQuery<IReadOnlyList<BanRecordResponse>>;
