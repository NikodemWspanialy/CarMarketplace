using CarMarketplace.Application.Admin.DTOs;
using CarMarketplace.Application.Users.Searchers;
using MediatR;

namespace CarMarketplace.Application.Admin.Queries.GetBanHistory;

internal class GetBanHistoryHandler(
    IUserSearcher userSearcher) : IRequestHandler<GetBanHistoryRequest, IReadOnlyList<BanRecordResponse>>
{
    public async Task<IReadOnlyList<BanRecordResponse>> Handle(GetBanHistoryRequest request, CancellationToken token)
    {
        var user = await userSearcher.FindByIdAsync(request.UserId, token);

        return user.BanHistory
            .OrderByDescending(b => b.BannedAt)
            .Select(BanRecordResponse.FromEntity)
            .ToList();
    }
}
