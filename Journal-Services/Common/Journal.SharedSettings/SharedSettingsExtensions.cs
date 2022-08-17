using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Journal.SharedSettings;

public static class SharedSettingsExtensions
{
    public static SettingsData AddSharedSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settingsData = new SettingsData(configuration);

        services.AddSingleton(settingsData);

        return settingsData;
    }
}