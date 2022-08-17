using System.Data;
using Journal.SharedSettings;
using Npgsql;

namespace Journal.Infrastructure.Database;

public class DapperContext
{
    private readonly SettingsData _settingsData;

    public DapperContext(SettingsData settingsData)
    {
        _settingsData = settingsData;
    }

    public IDbConnection GetConnection() => new NpgsqlConnection(_settingsData.Database.ConnectionString);
}