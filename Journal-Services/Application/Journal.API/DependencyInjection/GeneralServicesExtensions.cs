using Journal.Infrastructure.Database;

namespace Journal.API.DependencyInjection;

public static class GeneralServicesExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseContext>();
    }
}