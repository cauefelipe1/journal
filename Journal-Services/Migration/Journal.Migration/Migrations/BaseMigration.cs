namespace Journal.Migration.Migrations;

public abstract class BaseMigration: FluentMigrator.Migration
{
    protected SettingsData Settings { get; }

    protected BaseMigration(SettingsData settings) => Settings = settings;
}