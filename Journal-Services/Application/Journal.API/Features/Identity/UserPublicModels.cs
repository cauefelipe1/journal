using Journal.Domain.Base;
using Journal.Domain.Models.User;

namespace Journal.API.Features.Identity;


/// <summary>
/// Define the user data model for mobile applications
/// </summary>
public class UserDataMobileModel : IPublicModel<UserDataModel, UserDataMobileModel>
{
    /// <summary>
    /// The unique user identifier.
    /// </summary>
    /// <example>ce647e84-50a5-4699-bdaf-79394ad63ed4</example>
    public Guid Id { get; set; }

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

    /// <inheritdoc/>
    public static UserDataMobileModel FromModel(UserDataModel source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var instance = new UserDataMobileModel
        {
            Id = source.Id,
            UserType = source.UserType,
            Email = source.Email,
            Username = source.Username
        };

        return instance;
    }
}