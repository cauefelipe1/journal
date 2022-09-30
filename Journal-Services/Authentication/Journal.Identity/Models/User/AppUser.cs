using Microsoft.AspNetCore.Identity;

namespace Journal.Identity.Models.User;

/// <summary>
/// Defines an user of the application.
/// </summary>
public class AppUserModel : IdentityUser
{
    /// <summary>
    /// The secondary id to be used at the business level.
    /// </summary>
    /// <example>1</example>
    public int SecondaryId { get; set; }
}

/// <summary>
/// Defines the input for the user registration request.
/// </summary>
public class AppUserRegistrationInput
{
    /// <summary>
    /// The user name the user can use to login into the application.
    /// </summary>
    /// <example>funnyUserName</example>
    public string UserName { get; set; } = string.Empty;

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

public class UserLoginResult
{
    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}