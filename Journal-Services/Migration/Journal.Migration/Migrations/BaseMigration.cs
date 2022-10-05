using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

public abstract class BaseMigration: FluentMigrator.Migration
{
    protected const string ROOT_PATH_SQL_SCRIPTS = "Migrations/SqlScripts/";
    protected SettingsData Settings { get; }

    protected BaseMigration(SettingsData settings) => Settings = settings;

    protected IDictionary<string, object?> ToSnakeCase(object @object, params string[] columnsToSkip) =>
        SnakeCaseAdapter.ConvertToSnakeCase(@object, columnsToSkip);

    /// <summary>
    /// This method will work when the class name follows the following pattern: Part1_Part2.
    /// </summary>
    /// <returns>The class name inverted.</returns>
    protected string GetCurrentFileName()
    {
        string[] className = GetType().Name.Split('_');

        return $"{ className[1] }_{ className[0] }";
    }
}