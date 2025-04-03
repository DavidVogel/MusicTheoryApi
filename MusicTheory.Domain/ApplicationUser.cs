using Microsoft.AspNetCore.Identity;

namespace MusicTheory.Domain;

/// <summary>
/// Represents a user in the application
/// </summary>
public class ApplicationUser : IdentityUser
{
    // Additional profile fields can go here (if needed)
    // This ApplicationUser will inherit all Identity properties (like Id, UserName, Email, PasswordHash, etc.)
    // When a user is created, Identity will hash the password using a strong algorithm (PBKDF2 with a unique salt by default), so plaintext passwords are never saved.
}
