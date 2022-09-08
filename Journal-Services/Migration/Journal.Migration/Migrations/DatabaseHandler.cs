using Dapper;
using Journal.Migration.Extensions;
using Npgsql;

namespace Journal.Migration.Migrations;

public class DatabaseHandler {

    private readonly SettingsData _settingsData;

    public DatabaseHandler(SettingsData settingsData) => _settingsData = settingsData;

    public void CheckOrCreateDatabase()
    {
        using var con = new NpgsqlConnection(_settingsData.GetMasterDatabaseConnectionString());

        con.Open();

        bool dbExist = InternalCheckIfDatabaseExists(con);

        if (!dbExist)
            InternalCreateDatabase(con);
    }

    private bool InternalCheckIfDatabaseExists(NpgsqlConnection con)
    {
        const string CHECK_DB_SQL =
            "SELECT COUNT(1) > 0 AS db_exist FROM pg_catalog.pg_database WHERE datname = @DatabaseName";

        var param = new { _settingsData.Database.DatabaseName };

        bool dbExist = con.QuerySingle<bool>(CHECK_DB_SQL, param);

        return dbExist;
    }

    private void InternalCreateDatabase(NpgsqlConnection con)
    {
        string createDbSql = $"CREATE DATABASE {_settingsData.Database.DatabaseName}";

        var param = new { _settingsData.Database.DatabaseName };

        con.Execute(createDbSql, param);
    }

    private void DestroyDatabase()
    {
        using var con = new NpgsqlConnection(_settingsData.GetMasterDatabaseConnectionString());
        con.Open();

        bool dbExist = InternalCheckIfDatabaseExists(con);

        if (!dbExist)
            return;

        const string KILL_DB_ACTIVITY = "SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = @DatabaseName";
        string dropDbSql = $"DROP DATABASE {_settingsData.Database.DatabaseName}";

        var param = new { _settingsData.Database.DatabaseName };

        con.Execute(KILL_DB_ACTIVITY, param);
        con.Execute(dropDbSql);
    }

    public void ResetDatabase()
    {
        DestroyDatabase();
        CheckOrCreateDatabase();
    }

}