namespace CarMarketplace.Domain.Users;

public class PasswordResetToken
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool IsUsed { get; private set; }

    // EF Core
    private PasswordResetToken() { }

    public PasswordResetToken(Guid userId, string token, DateTime createdAt, DateTime expiresAt)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Token = token;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
    }

    public bool IsValid => !IsUsed && ExpiresAt > DateTime.UtcNow;

    public void MarkAsUsed() => IsUsed = true;
}
