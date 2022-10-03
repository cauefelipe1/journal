namespace Journal.Migration.Migrations;

[Migration(00001)]
public class SetupDatabase_00001 : BaseMigration
{
    public SetupDatabase_00001(SettingsData settings) : base(settings) { }

    public override void Up()
    {
        AddOrRemoveSchemas(MigrationDirection.Up);
    }

    private void AddOrRemoveSchemas(MigrationDirection direction)
    {
        string getOperation() => direction is MigrationDirection.Up ? "CREATE" : "DROP";

        string SQL = @$"
            {getOperation()} SCHEMA {Settings.Database.SearchPath};
            {getOperation()} SCHEMA {Journal.Identity.Constants.IDENTITY_DB_SCHEMA};";

        Execute.Sql(SQL);
    }

    public override void Down()
    {
        AddOrRemoveSchemas(MigrationDirection.Down);
    }
}