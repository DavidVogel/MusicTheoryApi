using System.ComponentModel.DataAnnotations;

namespace MusicTheory.Api.Models;

/// <summary>
/// Represents the data transfer object for user registration
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// User's email
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// User's password
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}
