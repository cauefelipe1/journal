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
    public static uint GetUserSecondaryId(this ControllerBase controller)
    {
        string? value = controller.GetClaimValue(Constants.JWT_USER_SECONDARY_ID_CLAIM);

        if (!string.IsNullOrEmpty(value))
            return uint.Parse(value);

        return 0;
    }

    /// <summary>
    /// Returns the id of a logged user.
    /// </summary>
    public static Guid GetUserId(this ControllerBase controller)
    {
        string? value = controller.GetClaimValue(Constants.JWT_USER_ID_CLAIM);

        if (!string.IsNullOrEmpty(value))
            return Guid.Parse(value);

        return Guid.Empty;
    }


    private static string? GetClaimValue(this ControllerBase controller, string claimName)
    {
        var claims = controller.User?.Claims;

        if (claims is not null)
        {
            string rawUserId = claims.Single(c =>
                string.Equals(c.Type, claimName) &&
                !string.IsNullOrWhiteSpace(c.Value)).Value;

            return rawUserId;
        }

        return null;
    }
}