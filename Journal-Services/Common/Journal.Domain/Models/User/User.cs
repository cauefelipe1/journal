namespace Journal.Domain.Models.User;

/// <summary>
/// Defines the possible type of users for the application.
/// </summary>
public enum UserType : byte
{
    /// <summary>
    /// Standard user.
    /// Usually an user in the free tier of the application.
    /// </summary>
    Standard = 1,

    /// <summary>
    /// Premium user.
    /// Usually an user in the paid tier.
    /// </summary>
    Premium = 2
}

/// <summary>
/// Defines the user data info to be consumed for consumer applications
/// </summary>
public class UserData
{
    /// <summary>
    /// The unique user identifier.
    /// </summary>
    /// <example>1</example>
    public long UserId { get; set; }

    /// <summary>
    /// The type of the user.
    /// <see cref="UserType"/>
    /// </summary>
    /// <example>Premium</example>
    public UserType UserType { get; set; }

    /// <summary>
    /// The email of the user.
    /// </summary>
    /// <example>user@server.com</example>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The username of the user.
    /// </summary>
    /// <example>user_name</example>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// THe name of the user.
    /// </summary>
    /// <example>Brian O'Conner</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The nickname of the user.
    /// </summary>
    /// <example>Brian</example>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// The name will be used to be shown in the UI.
    /// If the user provide a <see cref="Nickname"/> it is used, otherwise is used the <see cref="Name"/>
    /// </summary>
    /// <example>Brian</example>
    public string DisplayName { get; set; } = string.Empty;
}