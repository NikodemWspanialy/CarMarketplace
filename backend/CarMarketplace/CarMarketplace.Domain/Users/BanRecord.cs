using CarMarketplace.Domain.Abstractions;

namespace CarMarketplace.Domain.Users;

public class BanRecord : IEntity
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public Guid BannedByAdminId { get; private set; }

    public string Reason { get; private set; }

    public DateTime BannedAt { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    public DateTime? UnbannedAt { get; private set; }

    public string? UnbanReason { get; private set; }

    public Guid? UnbannedByAdminId { get; private set; }

    // EF Core
    private BanRecord() { }

    public BanRecord(Guid userId, Guid bannedByAdminId, string reason, DateTime bannedAt, DateTime? expiresAt = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        BannedByAdminId = bannedByAdminId;
        Reason = reason;
        BannedAt = bannedAt;
        ExpiresAt = expiresAt;
    }

    public void MarkUnbanned(DateTime unbannedAt, Guid unbannedByAdminId, string? reason = null)
    {
        UnbannedAt = unbannedAt;
        UnbannedByAdminId = unbannedByAdminId;
        UnbanReason = reason;
    }
}
