namespace Journal.Infrastructure.Features.HealthCheck;

public interface IHealthCheckRepository
{
    bool IsDatabaseAlive();
}