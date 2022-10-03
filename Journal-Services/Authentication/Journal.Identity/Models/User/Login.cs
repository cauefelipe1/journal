namespace Journal.Identity.Models.User;

/// <summary>
/// Defines the input for the user login request.
/// </summary>
public class UserLoginInput
{
    /// <summary>
    /// The user's email.
    /// </summary>
    /// <example>user@server.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's password.
    /// </summary>
    /// <example>This is a secret, you cannot know it.</example>
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Defines the result of an user login request.
/// </summary>
public class UserLoginResult
{
    /// <summary>
    /// The JWT token.
    /// If the attempt was successfully it contains the token, empty otherwise.
    /// </summary>
    /// <example>xxxxx.yyyyy.zzzzz</example>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// The refresh token.
    /// If the attempt was successfully it contains the refresh token, empty otherwise.
    /// </summary>
    /// <example>ererererjkejrejooosklksdlaklejksrjsoisorise</example>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// The errors occured during the login attempt.
    /// If the attempt WAS NOT successfully it contains the errors list, null otherwise.
    /// </summary>
    public IEnumerable<string>? Errors { get; set; }
}


/// <summary>
/// Defines the input for an user refresh token attempt.
/// </summary>
public class RefreshTokenInput
{
    /// <summary>
    /// The JWT token.
    /// </summary>
    /// <example>xxxxx.yyyyy.zzzzz</example>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// The refresh token.
    /// </summary>
    /// <example>ererererjkejrejooosklksdlaklejksrjsoisorise</example>
    public string RefreshToken { get; set; } = string.Empty;
}