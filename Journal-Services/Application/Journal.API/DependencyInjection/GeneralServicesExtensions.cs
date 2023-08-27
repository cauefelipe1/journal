using System.Globalization;
using System.Reflection;
using Journal.API.Configurations;
using Journal.Infrastructure.Database;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Journal.API.DependencyInjection;

/// <summary>
/// Defines extension methods to add configuration and general services into the dependency injection container.
/// </summary>
public static class GeneralServicesExtensions
{
    private static string Capitalize(this string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);

    /// <summary>
    /// Adds the all swagger dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                Constants.Swagger.GENERAL_API,
                new OpenApiInfo { Title = $"Journal {Constants.Swagger.GENERAL_API.Capitalize()} API", Version = "V3" });

            options.SwaggerDoc(
                Constants.Swagger.MOBILE_API,
                new OpenApiInfo { Title = $"Journal {Constants.Swagger.MOBILE_API.Capitalize()} API", Version = "V3" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Inform a valid JWT token",
                Name = "Authorization",
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            options.AddXmlDocumentation();

        });
    }

    private static void AddXmlDocumentation(this SwaggerGenOptions options)
    {
        string path = AppContext.BaseDirectory;
        string[] xmlsToAdd =
        {
            $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml",
            "Journal.Domain.xml",
            "Journal.Identity.xml"
        };

        foreach (string xml in xmlsToAdd)
        {
            string additional = Path.Combine(path, xml);

            if (File.Exists(additional))
            {
                options.IncludeXmlComments(additional);
            }
        }
    }

    /// <summary>
    /// Adds the all database access dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(optionsLifetime: ServiceLifetime.Singleton, contextLifetime: ServiceLifetime.Transient);
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