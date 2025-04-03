namespace MusicTheory.Api.Models;

/// <summary>
/// Represents the data transfer object for user login
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the user
    /// </summary>
    public string Password { get; set; } = null!;
}
