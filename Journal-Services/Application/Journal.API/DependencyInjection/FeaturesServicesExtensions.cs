using Journal.Infrastructure.Features.HealthCheck;

namespace Journal.API.DependencyInjection;

public static class FeaturesServicesExtensions
{
    public static void AddFeatures(this IServiceCollection services)
    {
         services.AddSingleton<IHealthCheckRepository, HealthCheckRepository>();
    }
}