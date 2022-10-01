namespace Journal.Identity.Models.User;

/// <summary>
/// Defines the input for the user registration request.
/// </summary>
public class AppUserRegistrationInput
{
    /// <summary>
    /// The user name the user can use to login into the application.
    /// </summary>
    /// <example>funnyUserName</example>
    public string UserName { get; set; } = default!;

    /// <summary>
    /// The user's email.
    /// </summary>
    /// <example>user@server.com</example>
    public string Email { get; set; } = default!;

    /// <summary>
    /// The user's password.
    /// </summary>
    /// <example>This is a secret, you cannot know it.</example>
    public string Password { get; set; } = default!;
}

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
    public string Token { get; set; } = default!;

    /// <summary>
    /// When <see cref="Success"/> is false it contains the list of errors that happened during the registration attempt.
    /// </summary>
    public IEnumerable<string>? Errors { get; set; }
}