using Dapper;
using Journal.Infrastructure.Database;

namespace Journal.Infrastructure.Features.HealthCheck;

public class HealthCheckRepository : IHealthCheckRepository
{
    private readonly DatabaseContext _dbContext;

    public HealthCheckRepository(DatabaseContext dbContext) => _dbContext = dbContext;

    private const string DB_ALIVE_QUERY = "SELECT 1 AS DB_ALIVE";
    public bool IsDatabaseAlive()
    {
        try
        {
            using var connection = _dbContext.GetConnection();

            int isAlive = connection.QuerySingleOrDefault<int>(DB_ALIVE_QUERY);

            return isAlive == 1;
        }
        catch
        {
            return false;
        }
    }
}