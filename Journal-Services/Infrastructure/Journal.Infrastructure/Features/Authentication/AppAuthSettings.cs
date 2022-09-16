using Microsoft.Extensions.Configuration;

namespace Journal.Infrastructure.Features.Authentication;

/// <summary>
/// Defines the settings to handle the application authentication.
/// </summary>
public class AppAuthSettings
{
    /// <summary>
    /// The JWT secret key.
    /// !!!!DO NOT SHARE IT WITH TEH WORLD!!!!
    /// </summary>
    /// <example>It is a secret, you cannot know it.</example>
    public string JwtSecret { get; } = default!;

    public AppAuthSettings(IConfiguration configuration)
    {
        if (configuration is not null)
        {
            JwtSecret = configuration["JwtSettings:JwtSecret"];
        }
    }
}