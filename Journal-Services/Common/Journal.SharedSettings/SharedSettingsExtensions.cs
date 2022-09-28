using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.SharedSettings;

/// <summary>
/// Defines extension methods to work with the Shared Settings.
/// </summary>
public static class SharedSettingsExtensions
{
    /// <summary>
    /// Adds an instance of <see cref="SettingsData"/> into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    /// <param name="configuration">Application configurations.</param>
    /// <returns>The <see cref="SettingsData"/> instance added into the services collection.</returns>
    public static SettingsData AddSharedSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settingsData = new SettingsData(configuration);

        services.AddSingleton(settingsData);

        return settingsData;
    }
}