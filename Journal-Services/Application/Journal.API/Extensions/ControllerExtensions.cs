using Journal.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Journal.API.Extensions;

/// <summary>
/// Defines extensions methods for Controller classes.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Returns the secondary id of a logged user.
    /// </summary>
    public static int GetUserSecondaryId(this ControllerBase controller)
    {
        var claims = controller.User?.Claims;

        if (claims is not null)
        {
            string rawUserId = claims.Single(c =>
                string.Equals(c.Type, Constants.JWT_USER_SECONDARY_ID_CLAIM) &&
                !string.IsNullOrWhiteSpace(c.Value)).Value;

            return string.IsNullOrEmpty(rawUserId) ? 0 : int.Parse(rawUserId);
        }

        return 0;
    }
}