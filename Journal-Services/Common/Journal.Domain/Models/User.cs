namespace Journal.Domain.Models;

/// <summary>
/// Represents an user inside the system
/// </summary>
public class User
{
    /// <summary>
    /// The username the user uses to login in the system
    /// </summary>
    /// <example>v.corleone</example>
    public string Username { get; set; } = default!;

    /// <summary>
    /// The full user name
    /// </summary>
    /// <example>Marlon Brando</example>
    public string FullName { get; set; } = default!;

    /// <summary>
    /// The nickname the user wants to use.
    /// </summary>
    /// <example>The Godfather</example>
    public string Nickname { get; set; } = default!;

    /// <summary>
    /// The user email
    /// </summary>
    /// <example>v.corleone@server.com</example>
    public string Email { get; set; } = default!;
}