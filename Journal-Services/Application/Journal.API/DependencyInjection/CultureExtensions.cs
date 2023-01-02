using System.Globalization;
using Journal.Localization.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Journal.API.DependencyInjection;

/// <summary>
/// Defines the extension method to work with localizations.
/// </summary>
public static class CultureExtensions
{
    /// <summary>
    /// Adds the Localization dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");
        services
            .AddControllersWithViews()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
    }

    /// <summary>
    /// Set up the application with the used translations.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    public static void UseAppLocalization(this WebApplication app)
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt"),
            new CultureInfo("es")
        };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            FallBackToParentCultures = true,
            FallBackToParentUICultures = true,
            RequestCultureProviders = new IRequestCultureProvider[] { new AspNetCultureProvider() }
        });
    }
}