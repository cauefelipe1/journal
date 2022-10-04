using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

public abstract class BaseMigration: FluentMigrator.Migration
{
    protected SettingsData Settings { get; }

    protected BaseMigration(SettingsData settings) => Settings = settings;

    protected IDictionary<string, object?> ToSnakeCase(object @object, params string[] columnsToSkip) =>
        SnakeCaseAdapter.ConvertToSnakeCase(@object, columnsToSkip);
}