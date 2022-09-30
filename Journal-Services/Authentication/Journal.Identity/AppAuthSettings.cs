using Microsoft.Extensions.Configuration;

namespace Journal.Identity;

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

    /// <summary>
    /// The JWT lifetime.
    /// </summary>
    /// <example>00:01:00</example>
    public TimeSpan JwtLifetime { get; }

    public AppAuthSettings(IConfiguration configuration)
    {
        if (configuration is not null)
        {
            var configSection = configuration.GetSection(Constants.JWT_SETTINGS_SECTION);

            JwtSecret = configSection[nameof(JwtSecret)];
            JwtLifetime = TimeSpan.Parse(configSection[nameof(JwtLifetime)]);
        }
    }
}