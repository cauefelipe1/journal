using System.Globalization;
using Journal.Localization.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Journal.API.DependencyInjection;

public static class CultureExtensions
{

    /// <summary>
    /// Adds the all features dependencies into the <see cref="IServiceCollection"/>.
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

    public static void UseAppLocalization(this WebApplication app)
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("pt-BR"),
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