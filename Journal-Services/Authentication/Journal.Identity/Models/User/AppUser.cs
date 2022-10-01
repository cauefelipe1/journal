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