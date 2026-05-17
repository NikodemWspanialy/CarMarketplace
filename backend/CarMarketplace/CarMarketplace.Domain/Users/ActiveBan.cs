namespace CarMarketplace.Domain.Users;

public class ActiveBan
{
    public string Reason { get; private set; }

    public DateTime BannedAt { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    // EF Core
    private ActiveBan() { }

    public ActiveBan(string reason, DateTime bannedAt, DateTime? expiresAt = null)
    {
        Reason = reason;
        BannedAt = bannedAt;
        ExpiresAt = expiresAt;
    }

    public bool IsExpired => ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow;
}
