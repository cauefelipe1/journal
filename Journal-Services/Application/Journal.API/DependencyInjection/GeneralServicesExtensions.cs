using System.Reflection;
using Journal.Infrastructure.Database;

namespace Journal.API.DependencyInjection;

/// <summary>
/// Defines extension methods to add configuration and general services into the dependency injection container.
/// </summary>
public static class GeneralServicesExtensions
{
    /// <summary>
    /// Adds the all swagger dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Adds the all database access dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseContext>();
    }

    /// <summary>
    /// Adds the all additional JSON configuration files into the <see cref="ConfigurationManager"/>.
    /// </summary>
    /// <param name="configuration"><see cref="ConfigurationManager"/> instance.</param>
    public static void AddAdditionalConfigFiles(this ConfigurationManager configuration)
    {
        const string SECRETS_FILE = "sharedSettings.Secrets.json";

        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        configuration.AddJsonFile( Path.Combine(path, SECRETS_FILE), true, false);
    }
}