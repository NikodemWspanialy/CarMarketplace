using CarMarketplace.Domain.Abstractions;
using CarMarketplace.Domain.Users.Exceptions;

namespace CarMarketplace.Domain.Users;

public class User(Guid id, string email, string passwordHash, UserRole role, DateTime createdAt)
    : IAggregateRoot
{
    public Guid Id { get; private set; } = id;
    
    public string Email { get; private set; } = email;
    
    public string PasswordHash { get; private set; } = passwordHash;
    
    public UserRole Role { get; private set; } = role;
    
    public DateTime CreatedAt { get; private set; } = createdAt;

    public void ChangePassword(string newPasswordHash, string oldPasswordHash)
    {
        if (newPasswordHash == oldPasswordHash)
        {
            throw new SamePasswordAsPrevious();
        }

        if (string.IsNullOrEmpty(newPasswordHash))
        {
            throw new InvalidPassword();
        }
    }

    public void PromoteToAdmin()
    {
        throw new NotImplementedException();
    }
}