namespace Journal.Identity.Models.Registration;

/// <summary>
/// Defines the result of a User registration attempt.
/// </summary>
public class UserRegistrationResult
{
    /// <summary>
    /// Indicates if the attempt was or not successfully.
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// When <see cref="Success"/> is true it contains the JWT used to access the system.
    /// </summary>
    /// <example>xxxxx.yyyyy.zzzzz</example>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// When <see cref="Success"/> is false it contains the list of errors that happened during the registration attempt.
    /// </summary>
    public IEnumerable<string>? Errors { get; set; }
}