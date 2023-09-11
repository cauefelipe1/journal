using Journal.Identity.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Journal.Identity.Extensions;

public static class UserManagerExtensions
{
    public static Task<AppUserModel?> FindBySecondaryIdAsync(this UserManager<AppUserModel>? userManager, uint secondaryId)
    {
        if (userManager is null)
            throw new ArgumentNullException(nameof(userManager), "the User manager is null.");

        return userManager.Users.SingleOrDefaultAsync(x => x.SecondaryId == secondaryId);
    }
}