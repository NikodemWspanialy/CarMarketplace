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