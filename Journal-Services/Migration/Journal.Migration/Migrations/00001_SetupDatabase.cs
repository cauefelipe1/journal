namespace Journal.Migration.Migrations;

[Migration(00001)]
public class SetupDatabase_00001 : FluentMigrator.Migration
{
    private readonly SettingsData _settings;

    public SetupDatabase_00001(SettingsData settings) => _settings = settings;

    public override void Up()
    {
        AddOrRemoveSchemas(MigrationDirection.Up);
    }

    private void AddOrRemoveSchemas(MigrationDirection direction)
    {
        string getOperation() => direction is MigrationDirection.Up ? "CREATE" : "DROP";

        string SQL = @$"
            {getOperation()} SCHEMA {_settings.Database.SearchPath};
            {getOperation()} SCHEMA identity;";

        Execute.Sql(SQL);
    }

    public override void Down()
    {
        AddOrRemoveSchemas(MigrationDirection.Down);
    }
}