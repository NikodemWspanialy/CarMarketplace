using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Users.Exceptions;

namespace CarMarketplace.Domain.Users;

public class User : IAggregateRoot
{
    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public UserRole Role { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public ActiveBan? ActiveBan { get; private set; }

    public bool IsBanned => ActiveBan is not null && !ActiveBan.IsExpired;

    public List<BanRecord> BanHistory { get; private set; } = [];

    public User(
        string email,
        string passwordHash,
        string firstName,
        string lastName,
        DateTime createdAt)
    {
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
        Role = UserRole.User;
        Id = Guid.NewGuid();
    }

    public void SetPassword(string newPasswordHash)
    {
        if (string.IsNullOrEmpty(newPasswordHash))
            throw new InvalidPassword();

        PasswordHash = newPasswordHash;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void ResetPassword(string newPasswordHash)
    {
        if (string.IsNullOrEmpty(newPasswordHash))
            throw new InvalidPassword();

        PasswordHash = newPasswordHash;
    }

    public void ChangeEmail(string newEmail)
    {
        if (Email == newEmail)
            throw new SameEmailAsCurrent();

        Email = newEmail;
    }

    public void PromoteToAdmin()
    {
        if (Role == UserRole.Admin)
            throw new UserAlreadyAdmin();

        Role = UserRole.Admin;
    }

    public void DemoteToUser()
    {
        if (Role == UserRole.User)
            throw new UserAlreadyRegular();

        Role = UserRole.User;
    }

    public void Delete()
    {
        if (IsDeleted)
            throw new UserAlreadyDeleted();

        IsDeleted = true;
    }

    public void Ban(string reason, Guid bannedByAdminId, DateTime? expiresAt = null)
    {
        if (ActiveBan is not null && !ActiveBan.IsExpired)
            throw new UserAlreadyBanned();

        var now = DateTime.UtcNow;
        ActiveBan = new ActiveBan(reason, now, expiresAt);
        BanHistory.Add(new BanRecord(Id, bannedByAdminId, reason, now, expiresAt));
    }

    public void Unban(Guid unbannedByAdminId, string? reason = null)
    {
        if (ActiveBan is null || ActiveBan.IsExpired)
            throw new UserNotBanned();

        var activeBanRecord = BanHistory.FirstOrDefault(b => b.BannedAt == ActiveBan.BannedAt && b.UnbannedAt is null);

        activeBanRecord?.MarkUnbanned(DateTime.UtcNow, unbannedByAdminId, reason);
        ActiveBan = null;
    }
}