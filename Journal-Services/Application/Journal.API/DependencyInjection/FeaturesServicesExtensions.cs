using Journal.Infrastructure.Features.Driver;
using Journal.Infrastructure.Features.HealthCheck;
using Journal.Infrastructure.Features.Vehicle;

namespace Journal.API.DependencyInjection;

/// <summary>
/// Defines extension methods to add the dependencies for all features the system has.
/// </summary>
public static class FeaturesServicesExtensions
{
    /// <summary>
    /// Adds the all features dependencies into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
    public static void AddFeatures(this IServiceCollection services)
    {
         services.AddSingleton<IHealthCheckRepository, HealthCheckRepository>();
         services.AddSingleton<IVehicleRepository, VehicleRepository>();
         services.AddScoped<IDriverRepository, DriverRepository>();
    }
}