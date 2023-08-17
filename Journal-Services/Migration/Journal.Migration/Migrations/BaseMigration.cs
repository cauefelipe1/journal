using Journal.Migration.Infrastructure;

namespace Journal.Migration.Migrations;

public abstract class BaseMigration: FluentMigrator.Migration
{
    protected const string ROOT_PATH_SQL_SCRIPTS = "Migrations/SqlScripts/";
    protected SettingsData Settings { get; }

    protected BaseMigration(SettingsData settings) => Settings = settings;

    protected IDictionary<string, object?> ToSnakeCase(object @object, params string[] columnsToSkip) =>
        SnakeCaseAdapter.ConvertToSnakeCase(@object, columnsToSkip);

    public override void Up()
    {
        InternalUp();
        SeedData();
    }

    /// <summary>
    /// The method that actually implements the migration Up logic.
    /// </summary>
    protected virtual void InternalUp() { }

    /// <summary>
    /// Execute whatever SQL scripts in the default scripts folder with the same file as the current class.
    /// If an different behaviour is required this method must be overriden.
    /// </summary>
    protected virtual void SeedData()
    {
        string filePath = $"{ROOT_PATH_SQL_SCRIPTS}{GetCurrentFileName()}.sql";
        if (!File.Exists(filePath))
            return;

        var parameters = new Dictionary<string, string>
        {
            { "Schema", Settings.Database.SearchPath },
            { "Identity_Schema", Identity.Constants.IDENTITY_DB_SCHEMA }
        };

        Execute.Script($"{ ROOT_PATH_SQL_SCRIPTS }{GetCurrentFileName()}.sql", parameters);
    }

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