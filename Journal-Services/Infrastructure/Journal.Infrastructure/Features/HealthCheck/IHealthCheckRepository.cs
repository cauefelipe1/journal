namespace Journal.Infrastructure.Features.HealthCheck;

/// <summary>
/// Defines the methods to check the database health.
/// </summary>
public interface IHealthCheckRepository
{
    /// <summary>
    /// Checks whether the database is up and running queries.
    /// </summary>
    /// <returns>True if the application could run the command, false otherwise.</returns>
    bool IsDatabaseAlive();
}