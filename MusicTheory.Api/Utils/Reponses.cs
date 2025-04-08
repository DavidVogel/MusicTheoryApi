namespace MusicTheory.Api.Utils;

/// <summary>
/// Represents the response returned after a user is created
/// </summary>
public class UserCreatedResponse
{
    /// <summary>
    /// Message indicating the result of the user creation
    /// </summary>
    public string Message { get; set; } = "User created successfully!";
}

/// <summary>
/// Authentication token response
/// </summary>
public class TokenResponse
{
    /// <summary>
    /// JWT authentication token
    /// </summary>
    public string Token { get; set; } = string.Empty;
}

/// <summary>
/// Wrapper for scale notes response.
/// </summary>
public class ScaleNotesResponse
{
    /// <summary>
    /// The list of note strings in the scale.
    /// </summary>
    public IEnumerable<string> Notes { get; set; } = new List<string>();
}
