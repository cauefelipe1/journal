using System.Text;
using Journal.Identity.Database;
using Journal.Identity.Features.Jwt;
using Journal.Identity.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
        InternalAddIdentityCore(services, configuration);
        InternalAddJwtToken(services, configuration);

        services.AddScoped<IJwtRepository, JwtRepository>();
    }

    private static void InternalAddIdentityCore(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IdentityDatabaseContext>();
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

    private static void InternalAddJwtToken(IServiceCollection services, IConfiguration configuration)
    {
        var authSettings = new AppAuthSettings(configuration);
        var tokenValidationParameters = GetTokenValidationParameters(authSettings);

        services.AddSingleton(authSettings);
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
    }

    private static TokenValidationParameters GetTokenValidationParameters(AppAuthSettings authSettings)
    {
        var result = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.JwtSecret)),
            //TODO: Fix that after the user registration and token generator is ready.
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };

#if DEBUG
        result.ClockSkew = TimeSpan.Zero;
#endif

        return result;
    }


}