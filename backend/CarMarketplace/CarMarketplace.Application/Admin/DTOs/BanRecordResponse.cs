using CarMarketplace.Domain.Users;

namespace CarMarketplace.Application.Admin.DTOs;

public record BanRecordResponse(
    Guid Id,
    Guid UserId,
    Guid BannedByAdminId,
    string Reason,
    DateTime BannedAt,
    DateTime? ExpiresAt,
    DateTime? UnbannedAt,
    Guid? UnbannedByAdminId,
    string? UnbanReason)
{
    public static BanRecordResponse FromEntity(BanRecord record) =>
        new(record.Id,
            record.UserId,
            record.BannedByAdminId,
            record.Reason,
            record.BannedAt,
            record.ExpiresAt,
            record.UnbannedAt,
            record.UnbannedByAdminId,
            record.UnbanReason);
}
