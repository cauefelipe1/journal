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
public class UserDataModel
{
    /// <summary>
    /// The unique user identifier.
    /// </summary>
    /// <example>ce647e84-50a5-4699-bdaf-79394ad63ed4</example>
    public Guid Id { get; set; }

    /// <summary>
    /// The secondary unique user identifier.
    /// </summary>
    /// <example>1</example>
    public uint SecondaryId { get; set; }

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
}