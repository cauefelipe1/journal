namespace Journal.Migration.Migrations;

[Migration(00001)]
public class SetupDatabase_00001 : FluentMigrator.Migration
{
    private readonly SettingsData _settings;

    public SetupDatabase_00001(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        string SQL = $"CREATE SCHEMA {_settings.Database.SearchPath};";

        Execute.Sql(SQL);
    }

    public override void Down()
    {
        string SQL = $"DROP SCHEMA {_settings.Database.SearchPath};";

        Execute.Sql(SQL);
    }
}