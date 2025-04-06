using System.ComponentModel.DataAnnotations;

namespace MusicTheory.Api.Models;

/// <summary>
/// Represents the data transfer object for user login
/// </summary>
public class LoginDto
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
