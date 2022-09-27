using Journal.Identity.Database;
using Journal.Identity.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.Identity.Extensions;

public static class IdentityExtensions
{
    /// <summary>
    /// Adds the all identity services dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration">Application configurations.</param>
    public static void AddApplicationIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IdentityDatabaseContext>();
        var builder = services.AddIdentityCore<AppUserModel>(q =>
        {
            //Password
            q.Password.RequireLowercase = false;
            q.Password.RequireUppercase = false;
            q.Password.RequireNonAlphanumeric = false;
            q.Password.RequireDigit = false;

            //User
            q.User.RequireUniqueEmail = true;
        });

        builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
        builder.AddEntityFrameworkStores<IdentityDatabaseContext>();

    }
}